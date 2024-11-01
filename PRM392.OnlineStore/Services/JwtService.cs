
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using PRM392.OnlineStore.Application.Interfaces;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using PRM392.OnlineStore.Domain.Entities.Models;

namespace PRM392.OnlineStore.Api.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secret = "from sonhohuu deptrai6mui with love";
        private readonly IUserRepository _userRepository;
        public JwtService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string CreateToken(int entityId, string role, string email)
        {
            var claims = new List<Claim>
        {

                 new Claim(JwtRegisteredClaimNames.Sub, entityId.ToString()),
                new(JwtClaimTypes.Email, email),
                new(ClaimTypes.Role, role)

        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Access token expiration
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
        public string CreateToken(string email, string roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Email, email),
                new(ClaimTypes.Role, roles)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateToken(string Id)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                expires: DateTime.Now.AddDays(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<object?> RefreshTokenAsync(TokenRequest tokenRequest)
        {
            var principal = GetPrincipalFromExpiredToken(tokenRequest.Token);
            var userEmail = principal?.FindFirst(JwtClaimTypes.Email)?.Value;

            if (userEmail == null)
            {
                return null; // Token is invalid or doesn't contain email
            }

            var user = await _userRepository.FindAsync(u => u.Email == userEmail);
            if (user == null || !user.IsRefreshTokenValid(tokenRequest.RefreshToken))
            {
                return null; // User not found or refresh token invalid
            }

            var newJwtToken = CreateToken(user.UserId, user.Role, user.Email);
            var newRefreshToken = GenerateRefreshToken();

            await _userRepository.UpdateRefreshTokenAsync(user, newRefreshToken, DateTime.UtcNow.AddDays(30));

            return new
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken
            };
        }

    }
}
