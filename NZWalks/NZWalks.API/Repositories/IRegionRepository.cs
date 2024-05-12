using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync(string? filterOn = null, string? filterQuery = null);
        Task<Region> GetByIdAsync(Guid id);
        Task<Region> CreateAsync(AddRegionDto dto);
        Task<Region> UpdateAsync(Guid id, UpdateRegionDto dto);
        Task<Region> DeleteAsync(Guid id);
    }
}
