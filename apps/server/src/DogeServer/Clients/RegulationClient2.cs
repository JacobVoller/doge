using DogeServer.Models.DTO;
using DogeServer.Models.Entities;
using DogeServer.Util;
using Newtonsoft.Json;

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
                //TODO: Retry 529s
                using HttpResponseMessage response = await _httpClient.GetAsync(path);
                response.EnsureSuccessStatusCode();
                json = await response.Content.ReadAsStringAsync();
                //json = JsonUtil.TrimQuotes(json);

                return JsonConvert.DeserializeObject<T>(json);
                //return JsonUtil.DeSerialize<T>(json);
            }
            catch (Exception exception)
            {
                ExceptionUtil.Rethrow(exception);
                throw; // fix compiler error
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<List<Outline>?> GetListOfTitles()
        {
            const string endpoint = "titles.json";
            var response = await Get<TitleResponse>(endpoint);

            return response?.Titles?.ToList();
        }

        public async Task<TitleStructure?> GetTitleStructure(string? date, string? title)
        {
            if (date == null) return default;
            if (title == null) return default;

            var endpoint = $"structure/{date}/title-{title}.json";
            return await Get<TitleStructure>(endpoint);
        }

        //public async Task<List<Section>?> GetSections(string? date, int? title)
        //{
        //    if (date == null) return default;
        //    if (title == null) return default;

        //    var endpoint = $"full/{date}/title-{title}.xml";
        //    var response = await Get<SectionResponse>(endpoint);

        //    return response?.ContentVersions?.ToList();
        //}
    }
}
