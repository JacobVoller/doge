
using DogeServer.Clients;
using DogeServer.Data;
using DogeServer.Models.DogeResponses;
using DogeServer.Models.DTO;
using DogeServer.Models.Entities;
using DogeServer.Util;

namespace DogeServer.Services;

public interface ISeedService
{
    Task<DogeResponse<string>> StartSeed();
}

public class SeedService(DataLake dataLake) : ISeedService
{
    protected readonly DataLake DataLake = dataLake;

    public static async Task Seed()
    {
        DataLake dataLake = new();
        ISeedService service = new SeedService(dataLake);

        await service.StartSeed();
    }

    public async Task<DogeResponse<string>> StartSeed()
    {
        AsyncUtil.FireAndForget(async () =>
        {
            EcfrApiClient client = new();
            await GetOutline(client);
        });

        return new DogeResponse<string>()
        {
            Results = "Seeding started."
        };
    }

    protected async Task GetOutline(EcfrApiClient client)
    {
        if (client == null) return;

        var titles = await client.GetListOfTitles();
        if (titles == null) return;
        if (titles.Count == 0) return;

        await Task.WhenAll(titles.Select(title => 
            GetTitleStructure(client, title)));

        await GenerateHierarchy();

        //TODO: Get actual regulations
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

    //TODO: rename
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

    protected async Task GenerateHierarchy()
    {
        var titles = await DataLake.Outline.GetTitles();
        //TODO : continue
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
