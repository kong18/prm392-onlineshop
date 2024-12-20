﻿using MediatR;
using PRM392.OnlineStore.Application.Carts.Commands;
using PRM392.OnlineStore.Application.Interfaces;
using PRM392.OnlineStore.Application.PayOs;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Orders.Commands
{
    public class CreateOrderCommand : IRequest<string>
    {
        public List<Item> CartItems { get; set; }
        public int LocationId { get; set; }
        public string BillingAddress { get; set; }
    }

    public class Item
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly PayOsServices _payOsServices;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, ICurrentUserService currentUserService, IUserRepository userRepository, PayOsServices payOsServices, IProductRepository productRepository, ICartRepository cartRepository, ICartItemRepository cartItemRepository)
        {
            _orderRepository = orderRepository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _payOsServices = payOsServices;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
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

            decimal totalPrice = 0;

            foreach (var item in request.CartItems)
            {
                var product = await _productRepository.FindAsync(x => x.ProductId == item.ProductId, cancellationToken);
                if (product is null)
                {
                    throw new NotFoundException($"Product with id {item.ProductId} does not exist");
                }

                totalPrice += product.Price * item.Quantity;
            }

            // Create a new cart with status "Completed" and the calculated total price
            var completedCart = new Cart
            {
                UserId = int.Parse(userId),
                Status = "Completed",
                TotalPrice = totalPrice
            };
            _cartRepository.Add(completedCart);
            await _cartRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            foreach (var item in request.CartItems)
            {
                var product = await _productRepository.FindAsync(x => x.ProductId == item.ProductId, cancellationToken);

                var cartItem = new CartItem
                {
                    CartId = completedCart.CartId, // Use the ID of the newly created cart
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price * item.Quantity
                };
                _cartItemRepository.Add(cartItem);
            }

            // Create a new order with the cart's details
            var order = new Domain.Entities.Models.Order
            {
                CartId = completedCart.CartId,
                UserId = int.Parse(userId),
                LocationId = request.LocationId,
                BillingAddress = request.BillingAddress,
                PaymentMethod = "Credit Card", // Example, could be passed in request or set dynamically
                OrderStatus = "Pending",
                OrderDate = DateTime.UtcNow
            };
            _orderRepository.Add(order);

            // Save changes to the cart and order
            await _cartRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _cartItemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var paymentRequest = new PaymentRequest
            {
                OrderId = order.OrderId,
                Amount = completedCart.TotalPrice
            };
            var paymentLink = await _payOsServices.CreatePaymentLink(paymentRequest);

            return paymentLink;
        }
    }

}
