
using DogeServer.Clients;
using DogeServer.Data;
using DogeServer.Models;
using DogeServer.Models.Entities;
using DogeServer.Util;

namespace DogeServer.Services
{
    public interface IDataRetrievalService
    {
        Task<DogeServiceControllerResponse<List<Title>>> Load();
    }

    public class DataRetrievalService(DataLake dataLake) : IDataRetrievalService
    {
        protected readonly DataLake DataLake = dataLake;

        public async Task<DogeServiceControllerResponse<List<Title>>> Load()
        {
            RegulationClient2 client = new(); //TODO: RENAME RegulationClient2
            var titles = await GetTitles(client);

            return new DogeServiceControllerResponse<List<Title>>()
            {
                Results = titles
            };
        }

        protected async Task<List<Title>?> GetTitles(RegulationClient2 client)
        {
            if (client == null) return default;

            var titles = await client.GetListOfTitles();
            if (titles == null) return default;
            if (titles.Count == 0) return default;

            foreach (var title in titles)
            {
                await DataLake.Title.Create(title);
            }

            await Task.WhenAll(titles.Select(item => GetTitleStructure(client, item)));

            //TODO: Testing
            //return await DataLake.Title.GetAll();
            
            return titles;
        }

        protected async Task GetTitleStructure(RegulationClient2 client, Title title)
        {
            if (title == null) return;

            var date = title?.LastIssued ?? title?.LastUpdated ?? title?.LastAmended;
            var titleNumber = title?.Number?.ToString();

            var titleStructure = await client.GetTitleStructure(date, titleNumber);
            if (titleStructure == null) return;

            EntityUtil.Zip(title, titleStructure);
            await DataLake.Title.CreateOrUpdate(title);

            //TODO: children
        }

        //TODO: This may not be needed with GetTitleStructure
        protected async Task<List<Section>?> GetSections(RegulationClient2 client, Title title)
        {
            if (title == null) return default;

            var titleID = title?.ID;
            if (titleID == Guid.Empty) return default;

            var sections = await client.GetSections(title?.LastAmended, title?.Number);
            if (sections == null) return default;

            foreach (var section in sections)
            {
                section.TitleID = titleID;
                
                await DataLake.Section.Create(section);
            }

            return sections;
        }
    }
}
