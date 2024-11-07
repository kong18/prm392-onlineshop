using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User, User, ApplicationDbContext>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }
        public string GeneratePassword()
        {
            var characters = "qwertyuiopasdfghjklzxcvbnm1234567890!@#$%";

            var random = new Random();

            StringBuilder sb = new StringBuilder();
            while (sb.Length < 7)
            {

                // Get a random index
                var index = random.Next(characters.Length);

                // Get character at index
                var character = characters[index];

                // Append to string builder
                sb.Append(character);
            }

            return sb.ToString();
        }
       
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
           
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public async Task UpdateRefreshTokenAsync(User user, string refreshToken, DateTime expiryTime)
        {
            user.SetRefreshToken(refreshToken, expiryTime);

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<User?> GetUserWithOrdersAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
            .Include(u => u.Orders)
            .ThenInclude(o => o.StoreLocation)
            .FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);
        }
        public async Task<List<User>> GetUsersByIdsAsync(List<int> userIds, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .Where(u => userIds.Contains(u.UserId))
                .ToListAsync(cancellationToken);
        }
    }
}
