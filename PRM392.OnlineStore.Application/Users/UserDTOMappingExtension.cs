using AutoMapper;
using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Users
{
    public static class UserDTOMappingExtention
    {
        public static UserDTO MapToUserDTO(this User projectFrom, IMapper mapper)
          => mapper.Map<UserDTO>(projectFrom);

        public static List<UserDTO> MapToUserDTOList(this IEnumerable<User> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToUserDTO(mapper)).ToList();
    }
}
