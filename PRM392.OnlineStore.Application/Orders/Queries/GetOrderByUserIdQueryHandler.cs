using AutoMapper;
using MediatR;
using PRM392.OnlineStore.Application.Interfaces;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Orders.Queries
{
    public class GetOrderByUserIdQuery : IRequest<List<OrderDTO>>
    {

    }
    public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrderByUserIdQuery, List<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper; // Optional, for mapping to DTO
        private readonly IUserRepository _userRepository;

        public GetOrderByUserIdQueryHandler(
            IOrderRepository orderRepository,
            ICurrentUserService currentUserService,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<OrderDTO>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId == null)
            {
                throw new UnauthorizedException("User not logged in");
            }

            var userExist = await _userRepository.FindAsync(x => x.UserId == int.Parse(userId), cancellationToken);

            if (userExist is null)
            {
                throw new NotFoundException("User does not exist");
            }

            // Query orders for the current user ID
            var orders = await _orderRepository.FindAllAsync(x => x.UserId == int.Parse(userId), cancellationToken);

            // Map to OrderDTO if using AutoMapper, or manually map if not
            return orders.MapToOrderDTOList(_mapper);
        }
    }
}
