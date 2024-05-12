using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public SQLWalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<Walk> CreateAsync(Walk model)
        {
            await nZWalksDbContext.Walks.AddAsync(model);
            await nZWalksDbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null,bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = nZWalksDbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering

            if(!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) //Ignore casing
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase)) //Ignore casing
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if( sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination

            var skipResults = (pageNumber - 1) * pageSize;  //Paging formular : works on small/large data sets

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();  
        }
        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await nZWalksDbContext.Walks.FindAsync(id);
        }

        public async Task<Walk> RemoveAsync(Guid id)
        {
            var walk = await nZWalksDbContext.Walks.FindAsync(id);

            nZWalksDbContext.Walks.Remove(walk);

            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk model)
        {
            var walk = await nZWalksDbContext.Walks.FindAsync(id);

            await nZWalksDbContext.SaveChangesAsync();

            walk.Name = model.Name;
            walk.Description = model.Description;
            walk.LengthInKm = model.LengthInKm;
            walk.Description = model.Description;
            model.DifficultyId = model.DifficultyId;
            model.RegionId = model.RegionId;

            await nZWalksDbContext.SaveChangesAsync();

            return walk;
        }
    }
}
