using PRM392.OnlineStore.Application.Common.DTO;
using PRM392.OnlineStore.Application.Common.Interfaces;
using PRM392.OnlineStore.Application.Models;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;

namespace PRM392.OnlineStore.Api.Services
{
    public class StoreLocationService : IStoreLocationService
    {
        private readonly IStoreLocationRepository _storeLocationRepository;

        public StoreLocationService(IStoreLocationRepository storeLocationRepository)
        {
            _storeLocationRepository = storeLocationRepository;
        }

        public async Task<StoreLocationDto?> GetStoreLocationAsync(int locationId)
        {
            var storeLocation = await _storeLocationRepository.GetStoreLocationAsync(locationId);
            if (storeLocation == null) return null;

            return new StoreLocationDto
            {
                Latitude = storeLocation.Latitude,
                Longitude = storeLocation.Longitude,
                Address = storeLocation.Address
            };
        }

        public string GetDirectionsUrl(decimal userLatitude, decimal userLongitude, int locationId)
        {
            var storeLocation = _storeLocationRepository.GetStoreLocationAsync(locationId).Result;
            if (storeLocation == null) return string.Empty;

            return $"https://www.google.com/maps/dir/?api=1&origin={userLatitude},{userLongitude}&destination={storeLocation.Latitude},{storeLocation.Longitude}";
        }
        public async Task<Result> AddStoreLocationAsync(StoreLocationDto storeLocationDto, CancellationToken cancellationToken = default)
        {
            var errors = new List<string>();

            // Validation for required fields
            if (string.IsNullOrEmpty(storeLocationDto.Address))
            {
                errors.Add("Address is required.");
            }

            // Check for valid latitude and longitude range
            if (storeLocationDto.Latitude < -90 || storeLocationDto.Latitude > 90)
            {
                errors.Add("Latitude must be between -90 and 90.");
            }
            if (storeLocationDto.Longitude < -180 || storeLocationDto.Longitude > 180)
            {
                errors.Add("Longitude must be between -180 and 180.");
            }

            // Return errors if any validations fail
            if (errors.Any())
            {
                return Result.Failure(errors);
            }

            var storeLocation = new StoreLocation
            {
                Latitude = storeLocationDto.Latitude,
                Longitude = storeLocationDto.Longitude,
                Address = storeLocationDto.Address
            };

            // Attempt to add to repository
            try
            {
                await _storeLocationRepository.AddStoreLocation(storeLocation, cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                errors.Add($"An error occurred while adding the store location: {ex.Message}");
                return Result.Failure(errors);
            }
        }
        public async Task<List<StoreLocationDto>> GetStoreLocationsByCoordinatesAsync(decimal? latitude, decimal? longitude, CancellationToken cancellationToken = default)
        {
            var storeLocations = await _storeLocationRepository.FindByCoordinatesAsync(latitude, longitude, cancellationToken);

            if (!storeLocations.Any())
            {
                throw new KeyNotFoundException("No store locations found with the specified coordinates.");
            }

            // Chuyển đổi sang DTO
            return storeLocations.Select(location => new StoreLocationDto
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Address = location.Address
            }).ToList();
        }

    }
}
