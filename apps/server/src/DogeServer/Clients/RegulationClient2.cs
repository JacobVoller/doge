using DogeServer.Models.DTO;
using DogeServer.Models.Entities;
using DogeServer.Util;

namespace DogeServer.Clients
{
    public class RegulationClient2
    {
        protected const string BaseUrl = "https://www.ecfr.gov/api/versioner/v1/";
        protected static readonly HttpClient _httpClient;
        protected readonly SemaphoreSlim _semaphore = new(5); //TODO: Magic Number

        static RegulationClient2()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        protected async Task<T?> Get<T>(string path)
        {
            string? json = string.Empty;
            
            await _semaphore.WaitAsync();
            try
            {
                Console.WriteLine(path); //TODO

                //TODO: Retry 529s
                using HttpResponseMessage response = await _httpClient.GetAsync(path);
                response.EnsureSuccessStatusCode();
                json = await response.Content.ReadAsStringAsync();
                json = JsonUtil.TrimQuotes(json);

                return JsonUtil.DeSerialize<T>(json);
            }
            catch
            {
                Console.WriteLine(json); //TODO
                return default;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<List<Title>?> GetTitles()
        {
            const string endpoint = "titles.json";
            var response = await Get<TitleResponse>(endpoint);

            return response?.Titles?.ToList();
        }

        public async Task<List<Section>?> GetSections(string? date, int? title)
        {
            if (date == null || title == null) return null;
            
            var endpoint = $"full/{date}/title-{title}.xml";
            var response = await Get<SectionResponse>(endpoint);

            return response?.ContentVersions?.ToList();
        }
    }
}
