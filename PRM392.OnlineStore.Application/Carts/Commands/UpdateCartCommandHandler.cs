using MediatR;
using PRM392.OnlineStore.Application.Common.Interfaces;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure.Repositories;

namespace PRM392.OnlineStore.Application.Common.Carts.Commands
{
    public class UpdateCartCommand : IRequest<string>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, string>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public UpdateCartCommandHandler(ICartRepository cartRepository, ICartItemRepository cartItemRepository, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var user = _currentUserService.UserId;
            if (user == null)
            {
                throw new UnauthorizedException("User not login");
            }
            var userExist = await _userRepository.FindAsync(x => x.UserId == int.Parse(user), cancellationToken);

            if (userExist is null)
            {
                throw new NotFoundException("User does not exist");
            }

            var cartExist = await _cartRepository.FindAsync(x => x.UserId == int.Parse(user) && x.Status == "Active", cancellationToken);
            if (cartExist is null)
            {
                throw new NotFoundException("No active cart found for the user");
            }

            var cartItemExist = await _cartItemRepository.FindAsync(
                x => x.CartId == cartExist.CartId && x.ProductId.Equals(request.ProductId),
                cancellationToken
            );

            if (cartItemExist is null)
            {
                throw new NotFoundException("Product not found in the cart");
            }

            // Update quantity and price of the cart item
            cartItemExist.Price = cartItemExist.Price / cartItemExist.Quantity * request.Quantity;
            cartItemExist.Quantity = request.Quantity;
            _cartItemRepository.Update(cartItemExist);

            // Update the total price of the cart
            cartExist.TotalPrice = await _cartItemRepository.GetCartTotalPrice(cartExist.CartId);
            _cartRepository.Update(cartExist);

            await _cartRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Cart item updated successfully";
        }
    }

}
