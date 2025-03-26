using DogeServer.Clients;
using DogeServer.Data;
using DogeServer.Models.DogeResponses;
using DogeServer.Models.DTO;
using DogeServer.Models.Entities;
using DogeServer.Util;

namespace DogeServer.Services;

public interface ISeedService
{
    DogeResponse<string> StartSeed();
}

public class SeedService() : ISeedService
{
    protected readonly DataLake DataLake = DataLakeUtil.Factory();

    public static void Seed()
    {
        new SeedService().StartSeed();
    }

    public DogeResponse<string> StartSeed()
    {
        const string operation = "SEED";

        AsyncUtil.FireAndForget(async () =>
        {
            DebugUtil.Log(operation, "Start");
            
            EcfrApiClient client = new();
            await GetOutline(client);

            DebugUtil.Log(operation, "Complete");
        });

        return new DogeResponse<string>()
        {
            Results = "Seeding started."
        };
    }

    protected async Task GetOutline(EcfrApiClient httpClient)
    {
        if (httpClient == null) return;

        var titles = await httpClient.GetListOfTitles();
        if (titles == null) return;
        if (titles.Count == 0) return;

        await Task.WhenAll(titles.Select(title => 
            GetTitleStructure(httpClient, title)));

        //TODO
        //await GenerateHierarchy();

        titles = await DataLake.Outline.GetTitles();
        await Task.WhenAll(titles.Select(title =>
            GetTitleContents(httpClient, title)));

        //TODO: Get actual regulations
    }

    protected async Task GetTitleStructure(EcfrApiClient httpClient, Outline intTitle)
    {
        if (intTitle == null) return;

        var urlComponents = intTitle.GetRequestComponents();
        var structure = await httpClient.GetTitleStructure(urlComponents.Item1, urlComponents.Item2);
        if (structure == null) return;

        var asyncTasks = await RecursivelyProcessOutline(structure, intTitle);
        Task.WaitAll(asyncTasks);
    }

    protected async Task GetTitleContents(EcfrApiClient httpClient, Outline intTitle)
    {
        if (intTitle == null) return;
        var urlComponents = intTitle.GetRequestComponents();

        var full = await httpClient.GetFullTitle(urlComponents.Item1, urlComponents.Item2);
        if (full == null) return;

        YamlUtil.CreateFile(full, urlComponents.Item2);

        if (full.Title != null)
        {
            ProcessXmlTitle(full.Title);
        }

        if (full.Volume != null)
        {
            var volume = full.Volume;
            //TODO: update volume

            ProcessXmlTitle(volume.Title);
        }
    }

    protected void ProcessXmlTitle(Div? title)
    {
        if (title == null) return;

        //TODO: update title

        if (title.Chapter == null) return;
        foreach (var chapter in title.Chapter)
        {
            //TODO: update chapter

            if (chapter.Subchapter == null) continue;
            foreach (var subchapter in chapter.Subchapter)
            {
                //TODO: update subchapter

                if (subchapter.Part == null) continue;
                foreach (var part in subchapter.Part)
                {
                    //TODO: update subchapter

                    if (part == null) continue;

                    var debugger = 0;
                }

            }
        }
    }

    protected async Task<List<Task>> RecursivelyProcessOutline(TitleStructure extTitle, Outline intTitle)
    {
        var returnTasks = new List<Task>();

        if (extTitle == null) return returnTasks;

        EntityUtil.Zip(intTitle, extTitle);
        await DataLake.Outline.CreateOrUpdate(intTitle);

        if (extTitle.Children == null) return returnTasks;
        if (extTitle.Children.Length == 0) return returnTasks;

        foreach (var child in extTitle.Children)
        {
            returnTasks.AddRange(RecursivelyProcessOutline(child, new Outline
            {
                ParentID = intTitle.ID
            }));
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
