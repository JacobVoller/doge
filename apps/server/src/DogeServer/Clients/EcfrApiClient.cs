using DogeServer.Config;
using DogeServer.Models.DTO;
using DogeServer.Models.Entities;
using DogeServer.Util;
using Newtonsoft.Json;

namespace DogeServer.Clients;

public class EcfrApiClient
{
    protected static readonly HttpClient _httpClient;
    protected readonly SemaphoreSlim _jsonSemaphore = new(AppConfiguration.eCFR.ConcurrentJsonRequests);
    protected readonly SemaphoreSlim _xmlSemaphore = new(AppConfiguration.eCFR.ConcurrentXmlRequests);

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
        
        await _jsonSemaphore.WaitAsync();
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
            _jsonSemaphore.Release();
        }
    }

    protected async Task<T?> GetXml<T>(string path, string? exportName = null)
    {
        await _xmlSemaphore.WaitAsync();
        try
        {
            //TODO: Retry 529s
            using HttpResponseMessage response = await _httpClient.GetAsync(path);
            response.EnsureSuccessStatusCode();
            var xmlStream = await response.Content.ReadAsStreamAsync();

            return XmlUtil.DeSerialize<T>(xmlStream, exportName);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"FAILED XML: {path}");
            Console.WriteLine($"            {exception.Message}");

            ExceptionUtil.Rethrow(exception);
            throw; // fix compiler error
        }
        finally
        {
            _xmlSemaphore.Release();
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

    public async Task<FullTitleXml?> GetFullTitle(string? date, string? title)
    {
        if (date == null) return default;
        if (title == null) return default;

        var endpoint = $"full/{date}/title-{title}.xml";
        return await GetXml<FullTitleXml>(endpoint, title);
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
