using DogeServer.Config;
using DogeServer.Models.DTO;
using DogeServer.Models.Entities;
using DogeServer.Util;
using Newtonsoft.Json;

namespace DogeServer.Clients;

public class EcfrApiClient
{
    protected static readonly HttpClient _httpClient;
    protected readonly SemaphoreSlim _semaphore = new(AppConfiguration.eCFR.ConcurrentRequests);

    static EcfrApiClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(AppConfiguration.eCFR.BaseUrl)
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

            return JsonConvert.DeserializeObject<T>(json);
            //return JsonUtil.DeSerialize<T>(json); //TODO: what's wrong with this function
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

    //TODO: Delete
    //public async Task<List<Section>?> GetSections(string? date, int? title)
    //{
    //    if (date == null) return default;
    //    if (title == null) return default;

    //    var endpoint = $"full/{date}/title-{title}.xml";
    //    var response = await Get<SectionResponse>(endpoint);

    //    return response?.ContentVersions?.ToList();
    //}
}
