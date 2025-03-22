
using DogeServer.Data.Managers;
using Microsoft.EntityFrameworkCore;

namespace DogeServer.Data
{
    public class DataLake
    {
        public readonly TitleManager Title;

        protected DatabaseContext DatabaseContext { get; set; }
        protected DbContextOptions<DatabaseContext> DatabaseOptions { get; set; }
        protected string DatabaseName { get; set; }

        public DataLake()
        {
            DatabaseContext = new DatabaseContext();

            Title = new(DatabaseConnection);
        }

        private DbContextOptions<DatabaseContext> ConfigurePostgresOptions()
        {
            DatabaseName = "doge"; //TODO
            var host = "doge"; //TODO
            var username = "doge"; //TODO
            var password = "doge"; //TODO

            return new DbContextOptionsBuilder<DatabaseContext>()
                .UseNpgsql($"Host={host};Database={DatabaseName};Username={username};Password={password};")
                .Options;
        }

        private DatabaseContext DatabaseConnection()
        {
            return new DatabaseContext(DatabaseOptions)
            {
                DatabaseName = DatabaseName
            };
        }
    }
}
