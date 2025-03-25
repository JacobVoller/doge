
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

public class SeedService() : ISeedService
{
    protected readonly DataLake DataLake = DataLakeUtil.Factory();

    public static async Task Seed()
    {
        await new SeedService().StartSeed();
    }

    public async Task<DogeResponse<string>> StartSeed()
    {
        AsyncUtil.FireAndForget(async () =>
        {
            EcfrApiClient client = new();
            await GetOutline(client);

            Console.WriteLine("SEED COMPLETE"); //TODO
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

        //TODO
        //await GenerateHierarchy();

        titles = await DataLake.Outline.GetTitles();
        await Task.WhenAll(titles.Select(title =>
            DownloadTitle(client, title)));

        //TODO: Get actual regulations
    }

    protected async Task GetTitleStructure(EcfrApiClient client, Outline outline)
    {
        if (outline == null) return;

        var urlComponents = outline.GetRequestComponents();
        var structure = await client.GetTitleStructure(urlComponents.Item1, urlComponents.Item2);
        if (structure == null) return;

        var asyncTasks = await RecursivelyProcessOutline(structure, outline);
        Task.WaitAll(asyncTasks);
    }

    protected async Task DownloadTitle(EcfrApiClient client, Outline outline)
    {
        if (outline == null) return;
        var urlComponents = outline.GetRequestComponents();

        var full = await client.GetFullTitle(urlComponents.Item1, urlComponents.Item2);
        if (full == null) return;

        YamlUtil.Serialize(full, urlComponents.Item2); //TODO

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

    protected async Task<List<Task>> RecursivelyProcessOutline(TitleStructure structure, Outline outline)
    {
        var returnTasks = new List<Task>();

        if (structure == null) return returnTasks;

        EntityUtil.Zip(outline, structure);
        await DataLake.Outline.CreateOrUpdate(outline);

        if (structure.Children == null) return returnTasks;
        if (structure.Children.Length == 0) return returnTasks;

        foreach (var child in structure.Children)
        {
            returnTasks.AddRange(RecursivelyProcessOutline(child, new Outline
            {
                ParentID = outline.ID
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
