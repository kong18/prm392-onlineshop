using AutoMapper;
using MediatR;
using PRM392.OnlineStore.Application.Interfaces;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Users.Authenticate
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserLoginDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IUserRepository userRepository, IJwtService jwtService, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<UserLoginDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(u => u.Username == request.Username, cancellationToken);

            if (user == null || !_userRepository.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new NotFoundException("Invalid username or password.");
            }
           

            var accessToken = _jwtService.CreateToken(user.UserId, user.Role, user.Email);
            var refreshToken = _jwtService.GenerateRefreshToken();

            await _userRepository.UpdateRefreshTokenAsync(user, refreshToken, DateTime.UtcNow.AddDays(30));

            var userLoginDto = _mapper.Map<UserLoginDTO>(user);
            userLoginDto.Token = accessToken;
            userLoginDto.RefreshToken = refreshToken;


            return userLoginDto;
        }
    }
}
