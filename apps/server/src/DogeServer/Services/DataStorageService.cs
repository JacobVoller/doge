using DogeServer.Data;

namespace DogeServer.Services
{
    public interface IDataStorageService
    {
    }

    public class DataStorageService : IDataStorageService
    {
        protected DataLake DataLake { get; set; }

        public DataStorageService()
        {
            DataLake = new DataLake();
        }
    }
}
