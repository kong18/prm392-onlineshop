using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Users.Authenticate
{
    public class LoginQuery : IRequest<UserLoginDTO>

    {

        public LoginQuery() { }

        public LoginQuery(string username, string password)
        {
            Username = username;
            Password = password;
        }
     
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
