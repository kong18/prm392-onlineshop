using MediatR;
namespace PRM392.OnlineStore.Application.Users.ChangePassword
{
    public class ChangePasswordCommand : IRequest<string>
    {
        public ChangePasswordCommand(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
        public ChangePasswordCommand()
        {

        }

        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
