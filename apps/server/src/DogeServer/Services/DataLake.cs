using DogeServer.Models;
using DogeServer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogeServer.Services
{
    public class DataLake : DbContext
    {

        public string? DatabaseName { get; set; }

        public DbSet<Title> Titles { get; set; }
        public DbSet<Section> Sections { get; set; }

        //DO NOT DELETE. Required for EF.
        public DataLake() : base(new DbContextOptionsBuilder<DataLake>()
            .UseNpgsql($"Host={"localhost"};Database={"doge"};Username={"doge"};Password={"doge"};")
            .Options
        )
        {
        }

        //DO NOT DELETE.
        public DataLake(DbContextOptions<DataLake> options) : base(options)
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
