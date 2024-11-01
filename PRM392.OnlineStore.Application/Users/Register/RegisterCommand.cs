using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Users.Register
{
    public class RegisterCommand : IRequest<UserLoginDTO>
    {
        public RegisterCommand() { }

        public RegisterCommand(string email, string username, string password, string repassword)
        {
            Email = email;
            Username = username;
            Password = password;
            Repassword = repassword;
          
        }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Repassword { get; set; }

     
    }
}
