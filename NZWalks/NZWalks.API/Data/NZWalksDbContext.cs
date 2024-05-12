using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Difficulties  : Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
              new Difficulty()
              {
                  Id = Guid.Parse("70cb9776-534c-495f-b738-f35b93d5442a"),
                  Name = "Easy"
              },
              new Difficulty()
              {
                  Id = Guid.Parse("80cb9776-534c-495f-b738-f35b93d5442b"),
                  Name = "Medium"

              },
              new Difficulty()
              {
                  Id = Guid.Parse("90cb9776-534c-495f-b738-f35b93d5442c"),
                  Name = "Hard"
              }
            };
            
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>()
            {
              new Region()
              {
                  Id = Guid.Parse("48BB5B6D-7FD4-414F-8B92-E05159683814"),
                  Name = "Mpumalanga",
                  Code = "MP",
                  RegionImageUrl = "http://images/MP"
              },
              new Region()
              {
                  Id = Guid.Parse("24B26D15-6332-4900-A5C0-BD01CAFFBBBC"),
                  Name = "Gauteng",
                  Code = "GP",
                  RegionImageUrl = "http://images/GP"

              },
              new Region()
              {
                  Id = Guid.Parse("7F23C3E1-4218-4023-8B21-26B596D5F9E3"),
                  Name = "Limpompo",
                  Code = "LP",
                  RegionImageUrl = "http://images/LP"
              }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
