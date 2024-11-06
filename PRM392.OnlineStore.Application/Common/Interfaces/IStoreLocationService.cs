using PRM392.OnlineStore.Application.Common.DTO;
using PRM392.OnlineStore.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Common.Interfaces
{
    public interface IStoreLocationService
    {
        Task<StoreLocationDto?> GetStoreLocationAsync(int locationId);
        string GetDirectionsUrl(decimal userLatitude, decimal userLongitude, int locationId);
        Task<Result> AddStoreLocationAsync(StoreLocationDto storeLocationDto, CancellationToken cancellationToken = default);
        Task<List<StoreLocationDto>> GetStoreLocationsByCoordinatesAsync(decimal? latitude, decimal? longitude, CancellationToken cancellationToken = default);
        Task<Result> UpdateStoreLocationAsync(int locationId, StoreLocationDto storeLocationDto);
        Task<Result> DeleteStoreLocationAsync(int locationId);
    }
}
