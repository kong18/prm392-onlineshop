using AutoMapper;
using MediatR;
using PRM392.OnlineStore.Application.Common.Interfaces;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Users.GetInfo
{
    public class GetUserInfoQuery : IRequestHandler<GetUserInfo, UserDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserInfoQuery(ICurrentUserService currentUserService, IMapper mapper, IUserRepository userRepository)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserInfo request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserIdAsInt;
            if (userId == null)
            {
                throw new UnauthorizedException("User not login");
            }

            // Find the customer by Id
            var user = await _userRepository.FindAsync(c => c.UserId == userId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
            var dto = user.MapToUserDTO(_mapper);

            return dto;
        }
    }
}
