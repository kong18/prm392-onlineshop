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
        string CreateToken(string ID, string roles, string email);
        string CreateToken(string email, string roles);
        string CreateToken(string subject, string role, int expiryDays);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GenerateRefreshToken();
       

    }
}
