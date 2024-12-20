﻿using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Repositories
{
    public interface IUserRepository : IEFRepository<User, User>
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
        string GeneratePassword();
        Task UpdateRefreshTokenAsync(User user, string newRefreshToken, DateTime expiryDate);
        Task<User?> GetUserWithOrdersAsync(int userId, CancellationToken cancellationToken = default);
        Task<List<User>> GetUsersByIdsAsync(List<int> userIds, CancellationToken cancellationToken = default);

    }
}
