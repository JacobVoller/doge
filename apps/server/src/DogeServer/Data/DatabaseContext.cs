using DogeServer.Config;
using DogeServer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogeServer.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Outline> Outline { get; set; }

    //DO NOT DELETE. Required for EF.
    public DatabaseContext() : base(new DbContextOptionsBuilder<DatabaseContext>()
        .UseNpgsql($"Host={AppConfiguration.Database.Host};"
            + $"Database={AppConfiguration.Database.Database};"
            + $"Username={AppConfiguration.Database.Username};"
            + $"Password={AppConfiguration.Database.Password};")
        .Options
    )
    {
    }

    //DO NOT DELETE. Required for schema generation.
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase(AppConfiguration.Database.Database);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        const string id = "ID";
        var baseEntity = typeof(Entity);
        var entities = modelBuilder.Model.GetEntityTypes();

        foreach (var entity in entities)
        {
            if (!baseEntity.IsAssignableFrom(entity.ClrType))
                continue;

            modelBuilder.Entity(entity.ClrType)
               .HasKey(id);

            modelBuilder.Entity(entity.ClrType)
                .Property(id)
                .ValueGeneratedOnAdd();
        }

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<T>? GetTable<T>() where T : Entity
    {
        return Set<T>();
    }
}
