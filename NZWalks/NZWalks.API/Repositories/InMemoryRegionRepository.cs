using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Repositories
{
    public class InMemoryRegionRepository : IRegionRepository
    {
        public Task<Region> CreateAsync(AddRegionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Region> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Region>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
        {
            return new List<Region>()
            {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = "CC",
                    Name = "Name",
                },
                new Region()
                {
                    Id= Guid.NewGuid(),
                    Name = "Next Name",
                    Code = "NC"
                }
            };
        }

        public Task<Region> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Region> UpdateAsync(Guid id, UpdateRegionDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
