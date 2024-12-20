﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRM392.OnlineStore.Application.Common.DTO;
using PRM392.OnlineStore.Application.Common.Interfaces;

namespace PRM392.OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreLocationController : ControllerBase
    {
        private readonly IStoreLocationService _storeLocationService;
        public StoreLocationController(IStoreLocationService storeLocationService)
        {
            _storeLocationService = storeLocationService;
        }
        [HttpGet("store-location")]
        public async Task<IActionResult> GetStoreLocation(int locationId)
        {
            var location = await _storeLocationService.GetStoreLocationAsync(locationId);
            if (location == null)
            {
                return NotFound("Store location not found.");
            }
            return Ok(location);
        }
        [HttpGet("store-locations")]
        public async Task<IActionResult> GetAllStoreLocations(CancellationToken cancellationToken = default)
        {
            var locations = await _storeLocationService.GetAllStoreLocationsAsync(cancellationToken);

            return Ok(locations);
        }
        [HttpGet("directions")]
        public IActionResult GetDirections([FromQuery] decimal userLatitude, [FromQuery] decimal userLongitude, int locationId)
        {
            var directionsUrl = _storeLocationService.GetDirectionsUrl(userLatitude, userLongitude, locationId);
            if (string.IsNullOrEmpty(directionsUrl))
            {
                return NotFound("Store location not found.");
            }
            return Ok(new { directionsUrl });
        }
        [HttpPost("add-store-location")]
        public async Task<IActionResult> AddStoreLocation([FromBody] StoreLocationDto storeLocationDto, CancellationToken cancellationToken = default)
        {
            var result = await _storeLocationService.AddStoreLocationAsync(storeLocationDto, cancellationToken);

            if (!result.Succeeded)
            {
                return BadRequest(new { errors = result.Errors });
            }

            return Ok("Store location added successfully.");
        }
        [HttpGet("search-by-coordinates")]
        public async Task<IActionResult> GetStoreLocationsByCoordinates(decimal? latitude, decimal? longitude)
        {
            try
            {
                var storeLocations = await _storeLocationService.GetStoreLocationsByCoordinatesAsync(latitude, longitude);
                return Ok(storeLocations);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("No store locations found with the specified coordinates.");
            }
        }
        [HttpPut("update-store-location/{locationId}")]
        public async Task<IActionResult> UpdateStoreLocation(int locationId, [FromBody] StoreLocationDto storeLocationDto)
        {
            var result = await _storeLocationService.UpdateStoreLocationAsync(locationId, storeLocationDto);

            if (!result.Succeeded)
            {
                return BadRequest(new { errors = result.Errors });
            }

            return Ok("Store location updated successfully.");
        }

        [HttpDelete("delete-store-location/{locationId}")]
        public async Task<IActionResult> DeleteStoreLocation(int locationId)
        {
            var result = await _storeLocationService.DeleteStoreLocationAsync(locationId);

            if (!result.Succeeded)
            {
                return BadRequest(new { errors = result.Errors });
            }

            return Ok("Store location deleted successfully.");
        }

    }
}
