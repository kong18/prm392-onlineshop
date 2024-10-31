using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Repositories
{
    public interface IStoreLocationRepository : IEFRepository<StoreLocation, StoreLocation>
    {
        Task<StoreLocation?> GetStoreLocationAsync(int locationId);
        Task AddStoreLocation(StoreLocation storeLocation, CancellationToken cancellationToken = default);
        Task<List<StoreLocation>> FindByCoordinatesAsync(decimal? latitude, decimal? longitude, CancellationToken cancellationToken = default);
    }
}
