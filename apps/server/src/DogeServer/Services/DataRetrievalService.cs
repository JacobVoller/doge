
using DogeServer.Clients;
using DogeServer.Data;
using DogeServer.Models;
using DogeServer.Models.Entities;

namespace DogeServer.Services
{
    public interface IDataRetrievalService
    {
        Task<DogeServiceControllerResponse<List<Title>>> Load();
    }

    public class DataRetrievalService : IDataRetrievalService
    {
        protected readonly DataLake DataLake;

        public DataRetrievalService(DataLake dataLake)
        {
            DataLake = dataLake;
        }

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

            var titles = await client.GetTitles();
            if (titles == null) return default;
            if (titles.Count == 0) return default;

            foreach (var title in titles)
            {
                await DataLake.Title.Create(title);
            }

            await Task.WhenAll(titles.Select(item => GetSections(client, item)));

            return titles;
        }
        
        protected async Task GetSections(RegulationClient2 client, Title title)
        {
            if (title == null) return;

            var titles = await client.GetSections(title?.LastAmended, title?.Number);


        }
    }
}
