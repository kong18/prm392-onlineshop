using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PRM392.OnlineStore.Application.Interfaces;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Users.GetInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserDTO>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserInfoQueryHandler(ICurrentUserService currentUserService, IUserRepository userRepository, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedException("User not logged in");
            }        
            var user = await _userRepository.GetUserWithOrdersAsync(int.Parse(userId), cancellationToken);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }      
            return _mapper.Map<UserDTO>(user);
        }
    }
}
