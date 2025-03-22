
using DogeServer.Clients;
using DogeServer.Data;
using DogeServer.Models;
using DogeServer.Models.DTO;
using DogeServer.Models.Entities;
using DogeServer.Util;

namespace DogeServer.Services
{
    public interface IDataRetrievalService
    {
        Task<DogeServiceControllerResponse<List<Outline>>> Load();
    }

    public class DataRetrievalService(DataLake dataLake) : IDataRetrievalService
    {
        protected readonly DataLake DataLake = dataLake;

        public async Task<DogeServiceControllerResponse<List<Outline>>> Load()
        {
            RegulationClient2 client = new(); //TODO: RENAME RegulationClient2
            var outline = await GetOutline(client);

            return new DogeServiceControllerResponse<List<Outline>>()
            {
                Results = outline
            };
        }

        protected async Task<List<Outline>?> GetOutline(RegulationClient2 client)
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

        protected async Task GetTitleStructure(RegulationClient2 client, Outline outline)
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
}
