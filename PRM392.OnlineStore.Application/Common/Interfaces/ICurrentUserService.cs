using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserEmail { get; }
        string? UserId { get; }
        Task<bool> IsInRoleAsync(string role);
        Task<bool> AuthorizeAsync(string policy);
    }
}
