
using MediatR;
using PRM392.OnlineStore.Application.Common.Interfaces;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Users.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userEmail = _currentUserService.UserEmail;
            var user = await _userRepository.FindAsync(x => x.Email == userEmail);

            if (user == null)
            {
                throw new NotFoundException($"User not found");
            }

            if (!_userRepository.VerifyPassword(request.OldPassword, user.PasswordHash))
            {
                throw new NotFoundException("Old password is incorrect");
            }

            user.PasswordHash = _userRepository.HashPassword(request.NewPassword);
            _userRepository.Update(user);

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Password changed successfully";
        }
    }
}
