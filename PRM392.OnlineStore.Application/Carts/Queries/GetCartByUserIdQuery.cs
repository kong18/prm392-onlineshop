using AutoMapper;
using MediatR;
using PRM392.OnlineStore.Application.Carts;
using PRM392.OnlineStore.Application.Common.Interfaces;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Repositories;


namespace PRM392.OnlineStore.Application.Common.Carts.Queries
{
    public class GetCartByUserIdQuery : IRequest<CartDTO>
    {
    }
    public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, CartDTO>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetCartByUserIdQueryHandler(ICartRepository cartRepository, IMapper mapper, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<CartDTO> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
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
            // Retrieve the active cart for the specified user
            var activeCart = await _cartRepository.FindAsync(
                x => x.UserId == int.Parse(userId) && x.Status == "Active",
                cancellationToken
            );

            if (activeCart == null)
            {
                return new CartDTO();
            }

            // Map the cart entity to the CartDTO
            return activeCart.MapToCartDTO(_mapper);
        }
    }

}
