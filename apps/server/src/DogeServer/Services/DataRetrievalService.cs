
using DogeServer.Clients;
using DogeServer.Models.Entities;

namespace DogeServer.Services
{
    
    public interface IDataRetrievalService
    {
        Task Load();
    }

    public class DataRetrievalService : IDataRetrievalService
    {
        public async Task Load()
        {
            RegulationClient2 client = new();
            await GetTitles(client);

            return;
        }

        protected async Task GetTitles(RegulationClient2 client)
        {
            if (client == null) return;

            var titles = await client.GetTitles();
            if (titles == null) return;
            if (titles.Length == 0) return;

            //TODO: insert into DB

            await Task.WhenAll(titles.Select(item => GetSections(client, item)));
        }
        
        protected async Task GetSections(RegulationClient2 client, Title title)
        {
            if (title == null) return;

            var titles = await client.GetSections(title?.LastAmended, title?.Number);


        }
        
        
        
        
        
        
        
        

    }
}
