using DogeServer.Config;
using DogeServer.Data.Managers;
using Microsoft.EntityFrameworkCore;

namespace DogeServer.Data;

public class DataLake
{
    public readonly OutlineManager Outline;

    protected DbContextOptions<DatabaseContext> DatabaseOptions { get; set; }

    public DataLake()
    {
        DatabaseOptions = AppConfiguration.Database.UseInMemoryDatabase
            ? ConfigureInMemoryDbOptions()
            : ConfigurePostgresOptions();

        Outline = new(DatabaseConnection);
    }

    private static DbContextOptions<DatabaseContext> ConfigureInMemoryDbOptions()
    {
        return new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: AppConfiguration.Database.Database)
            .Options;
    }

    private static DbContextOptions<DatabaseContext> ConfigurePostgresOptions()
    {
        return new DbContextOptionsBuilder<DatabaseContext>()
            .UseNpgsql($"Host={AppConfiguration.Database.Host};"
                + $"Database={AppConfiguration.Database.Database};"
                + $"Username={AppConfiguration.Database.Username};"
                + $"Password={AppConfiguration.Database.Password};")
            .Options;
    }

    private DatabaseContext DatabaseConnection()
    {
        return new DatabaseContext(DatabaseOptions);
        //TODO: DELETE
        //{
        //    DatabaseName = AppConfiguration.Database.Database
        //};
    }
}
