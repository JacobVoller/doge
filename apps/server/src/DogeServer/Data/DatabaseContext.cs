using DogeServer.Models;
using DogeServer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogeServer.Data
{
    public class DatabaseContext : DbContext
    {
        public string? DatabaseName { get; set; } //TODO
        public DbSet<Title> Titles { get; set; }
        public DbSet<Section> Sections { get; set; }

        //DO NOT DELETE. Required for EF.
        public DatabaseContext() : base(new DbContextOptionsBuilder<DatabaseContext>()
            .UseNpgsql($"Host={"localhost"};Database={"doge"};Username={"doge"};Password={"doge"};") //TODO: Hardcoded
            .Options
        )
        {
        }

        //DO NOT DELETE.
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
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
}
