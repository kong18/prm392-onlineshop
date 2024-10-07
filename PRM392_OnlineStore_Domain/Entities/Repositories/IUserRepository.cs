using PRM392.OnlineStore.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Repositories
{
    public interface IUserRepository : IEFRepository<UserEntity, UserEntity>
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
        string GeneratePassword();

    }
}
