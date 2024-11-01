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
    public class UserDto : IMapFrom<User>
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>();
        }
    }
}
