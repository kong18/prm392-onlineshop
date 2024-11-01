using AutoMapper;
using PRM392.OnlineStore.Application.Common.Mappings;
using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Users
{
    public class UserLoginDTO : IMapFrom<User>
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public int EntityId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserLoginDTO>();
        }
    }
}
