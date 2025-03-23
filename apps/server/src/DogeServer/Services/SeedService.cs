
using DogeServer.Clients;
using DogeServer.Data;
using DogeServer.Models.DogeResponses;
using DogeServer.Models.DTO;
using DogeServer.Models.Entities;
using DogeServer.Util;

namespace DogeServer.Services;

public interface ISeedService
{
    Task<DogeResponse<List<Outline>>> Load();
}

public class SeedService(DataLake dataLake) : ISeedService
{
    protected readonly DataLake DataLake = dataLake;

    public async Task<DogeResponse<List<Outline>>> Load()
    {
        EcfrApiClient client = new();
        var outline = await GetOutline(client);

        return new DogeResponse<List<Outline>>()
        {
            Results = outline
        };
    }

    protected async Task<List<Outline>?> GetOutline(EcfrApiClient client)
    {
        if (client == null) return default;

        var titles = await client.GetListOfTitles();
        if (titles == null) return default;
        if (titles.Count == 0) return default;

        await Task.WhenAll(titles.Select(title => 
            GetTitleStructure(client, title)));

        //TODO: Testing
        return await DataLake.Outline.GetAll();
        
        //return titles;
    }

    protected async Task GetTitleStructure(EcfrApiClient client, Outline outline)
    {
        if (outline == null) return;

        var date = 
            outline?.LastIssued 
            ?? outline?.LastUpdated 
            ?? outline?.LastAmended;
        var titleNumber = outline?.Number?.ToString();

        var structure = await client.GetTitleStructure(date, titleNumber);
        if (structure == null) return;

        Task.WaitAll(Recur(structure, outline));
    }

    protected List<Task> Recur(TitleStructure structure, Outline? outline = null)
    {
        var returnTasks = new List<Task>();

        if (structure == null) return returnTasks;

        outline ??= new Outline();
        EntityUtil.Zip(outline, structure);

        returnTasks.Add(DataLake.Outline.CreateOrUpdate(outline));

        if (structure.Children == null) return returnTasks;
        if (structure.Children.Length == 0) return returnTasks;

        foreach (var child in structure.Children)
        {
            returnTasks.AddRange(Recur(child));
        }

        return returnTasks;
    }

    //TODO: This may not be needed with GetTitleStructure
    //protected async Task<List<Section>?> GetSections(RegulationClient2 client, Title title)
    //{
    //    if (title == null) return default;

    //    var titleID = title?.ID;
    //    if (titleID == Guid.Empty) return default;

    //    var sections = await client.GetSections(title?.LastAmended, title?.Number);
    //    if (sections == null) return default;

    //    foreach (var section in sections)
    //    {
    //        section.TitleID = titleID;
            
    //        await DataLake.Section.Create(section);
    //    }

    //    return sections;
    //}
}
