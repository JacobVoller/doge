using DogeServer.Data.Managers;
using Microsoft.EntityFrameworkCore;

namespace DogeServer.Data
{
    public class DataLake
    {
        public readonly TitleManager Title;
        public readonly SectionManager Section;

        protected string DatabaseName { get; set; } = "doge"; //TODO
        protected DbContextOptions<DatabaseContext> DatabaseOptions { get; set; }
        //protected DatabaseContext DatabaseContext { get; set; } //TODO

        public DataLake(bool useInMemoryDb = true)
        {
            //DatabaseContext = new DatabaseContext(); //TODO
            DatabaseOptions = useInMemoryDb
                ? ConfigureInMemoryDbOptions()
                : ConfigurePostgresOptions();

            Title = new(DatabaseConnection);
            Section = new(DatabaseConnection);
        }

        private DbContextOptions<DatabaseContext> ConfigureInMemoryDbOptions()
        {
            return new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: DatabaseName)
                .Options;
        }

        private DbContextOptions<DatabaseContext> ConfigurePostgresOptions()
        {
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
