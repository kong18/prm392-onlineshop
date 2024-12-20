﻿using MediatR;
using PRM392.OnlineStore.Application.Interfaces;
using PRM392.OnlineStore.Application.PayOs;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Carts.Commands
{
    public class CheckoutCartCommand : IRequest<string>
    {
        public int LocationId { get; set; }
        public string BillingAddress { get; set; }
    }
    public class CheckoutCartCommandHandler : IRequestHandler<CheckoutCartCommand, string>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly PayOsServices _payOsServices;
        private readonly IStoreLocationRepository _storeLocationRepository;

        public CheckoutCartCommandHandler(ICartRepository cartRepository, IOrderRepository orderRepository, ICurrentUserService currentUserService, IUserRepository userRepository, PayOsServices payOsServices, IStoreLocationRepository storeLocationRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _payOsServices = payOsServices;
            _storeLocationRepository = storeLocationRepository;
        }

        public async Task<string> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
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

            // Get the active cart for the user
            var cart = await _cartRepository.FindAsync(x => x.UserId == int.Parse(userId) && x.Status == "Active", cancellationToken);

            if (cart == null)
            {
                throw new NotFoundException("No active cart found for the user");
            }

            var location = await _storeLocationRepository.FindAsync(x => x.LocationId == request.LocationId, cancellationToken);

            if (location == null)
            {
                throw new NotFoundException("No location found");
            }

            // Update the cart status to "Completed"
            cart.Status = "Completed";
            _cartRepository.Update(cart);

            // Create a new order with the cart's details
            var order = new Domain.Entities.Models.Order
            {
                CartId = cart.CartId,
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
            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var paymentRequest = new PaymentRequest
            {
                OrderId = order.OrderId,
                Amount = cart.TotalPrice
            };
            var paymentLink = await _payOsServices.CreatePaymentLink(paymentRequest);

            return paymentLink;
        }
    }

}