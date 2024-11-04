using AutoMapper;
using PRM392.OnlineStore.Application.Mappings;
using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Users
{
    public class UserDTO : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Username {  get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDTO>();
        }
    }
}
