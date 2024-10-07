using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using PRM392.OnlineStore.Application.Interfaces;
using System.Security.Claims;

namespace PRM392_OnlineStore.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal? _claimsPrincipal;
        private readonly IAuthorizationService _authorizationService;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            _claimsPrincipal = httpContextAccessor?.HttpContext?.User;
            _authorizationService = authorizationService;
        }

        public string? UserEmail => _claimsPrincipal?.FindFirst(JwtClaimTypes.Email)?.Value;

        public string? UserId => _claimsPrincipal?.FindFirst(JwtClaimTypes.Subject)?.Value;

        public async Task<bool> AuthorizeAsync(string policy)
        {
            if (_claimsPrincipal == null) return false;
            return (await _authorizationService.AuthorizeAsync(_claimsPrincipal, policy)).Succeeded;
        }

        public async Task<bool> IsInRoleAsync(string role)
        {
            return await Task.FromResult(_claimsPrincipal?.IsInRole(role) ?? false);
        }
    }
}
