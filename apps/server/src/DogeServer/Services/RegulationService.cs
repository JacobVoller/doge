
using DogeServer.Clients;

namespace DogeServer.Services
{
    
    public interface IRegulationService
    {
        Task Load();
        Task Get();
    }

    public class RegulationService : IRegulationService
    {

        public async Task Get()
        {
            RegulationClient2 client = new();
            var x = await client.GetTitles();

            return;
        }

        public async Task Load()
        {
            throw new NotImplementedException();
        }
    }
}
