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

namespace PRM392.OnlineStore.Application.Carts.Commands
{
    public class DeleteCartCommand : IRequest<string>
    {
        public int ProductId { get; set; }
    }

    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, string>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public DeleteCartCommandHandler(ICartRepository cartRepository, ICartItemRepository cartItemRepository, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
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

            // Remove the cart item
            _cartItemRepository.Remove(cartItemExist);

            // Update the total price of the cart
            cartExist.TotalPrice = await _cartItemRepository.GetCartTotalPrice(cartExist.CartId);
            _cartRepository.Update(cartExist);

            await _cartRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Product removed from cart successfully";
        }
    }

}
