
using DogeServer.Models;
using Newtonsoft.Json;
using System;
using System.Buffers.Text;
using System.Net.Http;
using System.Text.Json;

namespace DogeServer.Clients
{



    public class RegulationClient2
    {
        protected const string BaseUrl = "https://www.ecfr.gov/api/versioner/v1/";
        protected static readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions JsonOptions = new() { 
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true
        };

        static RegulationClient2()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        protected static string TrimQuotes(string input)
        {
            if (input.Length >= 2 && input[0] == '"' && input[^1] == '"')
            {
                return input.Substring(1, input.Length - 2);
            }

            return input;
        }


        protected async Task<T?> Get<T>(string path)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(path);
            response.EnsureSuccessStatusCode();
            string? json = await response.Content.ReadAsStringAsync();
            json = TrimQuotes(json);
            //json = JsonConvert.DeserializeObject<string>(json);

            return JsonConvert.DeserializeObject<T>(json);
        }


        public async Task<Title[]?> GetTitles()
        {
            const string endpoint = "titles.json";
            var response = await Get<TitleResponse>(endpoint);

            return response?.titles;
        }
    
    }


}
