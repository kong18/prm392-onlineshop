using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Infrastructure.Repositories
{
    public class StoreLocationRepository : RepositoryBase<StoreLocation, StoreLocation, ApplicationDbContext>, IStoreLocationRepository
    {
        public StoreLocationRepository(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper) { }

        public async Task<StoreLocation?> GetStoreLocationAsync(int locationId)
        {
            return await FindAsync(location => location.LocationId == locationId);
        }
        public async Task<List<StoreLocation>> GetAllStoreLocationsAsync(CancellationToken cancellationToken = default)
        {
            return await FindAllAsync(cancellationToken);
        }
        public async Task AddStoreLocation(StoreLocation storeLocation, CancellationToken cancellationToken = default)
        {
            Add(storeLocation);
            await SaveChangesAsync(cancellationToken);
        }
        public async Task<List<StoreLocation>> FindByCoordinatesAsync(decimal? latitude, decimal? longitude, CancellationToken cancellationToken = default)
        {
            Expression<Func<StoreLocation, bool>> filterExpression;
            if (latitude.HasValue && longitude.HasValue)
            {
                filterExpression = location => location.Latitude == latitude && location.Longitude == longitude;
            }
            else if (latitude.HasValue)
            {
                filterExpression = location => location.Latitude == latitude;
            }
            else if (longitude.HasValue)
            {
                filterExpression = location => location.Longitude == longitude;
            }
            else
            {
                return new List<StoreLocation>();
            }

            var storeLocations = await FindAllAsync(filterExpression, cancellationToken);

            return storeLocations;
        }
        public async Task UpdateStoreLocationAsync(StoreLocation storeLocation)
        {
            Update(storeLocation);
            await SaveChangesAsync();
        }

        public async Task DeleteStoreLocationAsync(int locationId)
        {
            var storeLocation = await GetStoreLocationAsync(locationId);
            if (storeLocation != null)
            {
                Remove(storeLocation);
                await SaveChangesAsync();
            }
        }
    }
}
