using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public IMapper Mapper { get; }

        public SQLRegionRepository(NZWalksDbContext nZWalksDbContext, IMapper mapper)
        {
            this.nZWalksDbContext = nZWalksDbContext;
            Mapper = mapper;
        }
        public async Task<IEnumerable<Region>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetByIdAsync(Guid id)
        {
            return await nZWalksDbContext.Regions.FindAsync(id);
        }
        public async Task<Region> CreateAsync(AddRegionDto dto)
        {
            var region = new Region()
            {
                Code = dto.Code,
                Name = dto.Name,
                RegionImageUrl = dto.RegionImageUrl
            };

            Mapper.Map<Region>(region);

            await nZWalksDbContext.AddAsync(region);

            await nZWalksDbContext.SaveChangesAsync();  

           return region;
        }
        public async Task<Region> UpdateAsync(Guid id, UpdateRegionDto dto)
        {
            var region = await nZWalksDbContext.Regions.FindAsync(id);

            if (region == null)
            {
                return null;
            }

            Mapper.Map<UpdateRegionDto, Region>(dto, region);

            await nZWalksDbContext.SaveChangesAsync();

            return region;
        }
        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await nZWalksDbContext.Regions.FindAsync(id);

            nZWalksDbContext.Regions.Remove(region);

            await nZWalksDbContext.SaveChangesAsync();

            return region;
        }
    }
}
