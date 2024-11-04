using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Interfaces
{
    public interface IJwtService
    {
        string CreateToken(int ID, string roles, string email);
        string CreateToken(string email, string roles);
      
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GenerateRefreshToken();
        Task<object?> RefreshTokenAsync(TokenRequest tokenRequest);


    }
}
