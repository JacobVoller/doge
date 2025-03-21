using DogeServer.Models.DTO;
using DogeServer.Models.Entities;
using Newtonsoft.Json;
using System.Text.Json;

namespace DogeServer.Clients
{
    public class RegulationClient2
    {
        protected const string BaseUrl = "https://www.ecfr.gov/api/versioner/v1/";
        protected static readonly HttpClient _httpClient;
        protected readonly SemaphoreSlim _semaphore =  new(5);

        //protected readonly JsonSerializerOptions JsonOptions = new() { 
        //    PropertyNameCaseInsensitive = true,
        //    AllowTrailingCommas = true
        //};

        static RegulationClient2()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        protected static string? TrimQuotes(string? input)
        {
            if (input == null) return input;
            
            var startsAndEndsWithQuote = input.Length >= 2 && input[0] == '"' && input[^1] == '"';
            return startsAndEndsWithQuote
                ? input.Substring(1, input.Length - 2)
                : input;
        }

        protected async Task<T?> Get<T>(string path)
        {
            string? json = "";
            
            
            await _semaphore.WaitAsync();
            try
            {
                Console.WriteLine(path); //TODO

                //TODO: Retry 529s
                using HttpResponseMessage response = await _httpClient.GetAsync(path);
                response.EnsureSuccessStatusCode();
                json = await response.Content.ReadAsStringAsync();
                json = TrimQuotes(json);

                return JsonConvert.DeserializeObject<T>(json);
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

        public async Task<Title[]?> GetTitles()
        {
            const string endpoint = "titles.json";
            var response = await Get<TitleResponse>(endpoint);

            return response?.titles;
        }

        public async Task<Section[]?> GetSections(string date, int? title)
        {
            var endpoint = $"full/{date}/title-{title}.xml";
            var response = await Get<SectionResponse>(endpoint);

            return response?.content_versions;
        }
    }
}
