using MediatR;
using PRM392.OnlineStore.Application.Interfaces;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Carts.Commands
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

        public UpdateCartCommandHandler(ICartRepository cartRepository, ICartItemRepository cartItemRepository, ICurrentUserService currentUserService)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var user = _currentUserService.UserId;
            if (user == null)
            {
                throw new UnauthorizedException("User not login");
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
