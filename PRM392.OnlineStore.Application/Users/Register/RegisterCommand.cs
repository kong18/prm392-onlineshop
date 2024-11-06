using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Users.Register
{
    public class RegisterCommand : IRequest<string>
    {
        public RegisterCommand() { }

        public RegisterCommand(string email, string password, string repassword)
        {
            Email = email;
            Password = password;
            Repassword = repassword;
         
        }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
