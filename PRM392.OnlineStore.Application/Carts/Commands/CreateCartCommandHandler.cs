using MediatR;
using PRM392.OnlineStore.Application.Interfaces;
using PRM392.OnlineStore.Application.Products.Create;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Carts.Commands
{
    public class CreateCartCommand : IRequest<string>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, string>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public CreateCartCommandHandler(ICartRepository cartRepository, ICartItemRepository cartItemRepository, ICurrentUserService currentUserService, IUserRepository userRepository, IProductRepository productRepository)
        {
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<string> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var user = _currentUserService.UserId;
            if (user == null)
            {
                throw new UnauthorizedException("User not login");
            }

            var userExist = await _userRepository.FindAsync(x => x.UserId.Equals(user), cancellationToken);

            if (userExist is null)
            {
                throw new NotFoundException("User does not exist");
            }

            var productExist = await _productRepository.FindAsync(x => x.ProductId.Equals(request.ProductId), cancellationToken);

            if (productExist is null)
            {
                throw new NotFoundException($"Product with id {request.ProductId} does not exist");
            }

            // Check if an active cart already exists for the user
            var cartExist = await _cartRepository.FindAsync(x => x.UserId == int.Parse(user) && x.Status == "Active", cancellationToken);
            if (cartExist is null)
            {
                // Create a new cart for the user
                var cartNew = new Cart
                {
                    UserId = int.Parse(user),
                    Status = "Active",
                    TotalPrice = 0 // Initialize the total price to zero
                };
                _cartRepository.Add(cartNew);
                await _cartRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                cartExist = cartNew; // Assign the newly created cart to `cartExist`
            }

            // Check if the product is already in the cart
            var cartItemExist = await _cartItemRepository.FindAsync(
                x => x.CartId == cartExist.CartId && x.ProductId.Equals(request.ProductId),
                cancellationToken
            );

            if (cartItemExist is null)
            {
                // Add new product to cart
                var cartItem = new CartItem
                {
                    CartId = cartExist.CartId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    Price = productExist.Price * request.Quantity
                };
                _cartItemRepository.Add(cartItem);
            }
            else
            {
                // Update the quantity and price of the existing cart item
                cartItemExist.Quantity += request.Quantity;
                cartItemExist.Price += productExist.Price * request.Quantity;
                _cartItemRepository.Update(cartItemExist);
            }

            // Update the total price of the cart
            cartExist.TotalPrice += productExist.Price * request.Quantity;
            _cartRepository.Update(cartExist);

            await _cartRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Product added to cart successfully";
        }

    }
}
