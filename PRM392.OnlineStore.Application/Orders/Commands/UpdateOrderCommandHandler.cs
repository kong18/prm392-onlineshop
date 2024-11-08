using MediatR;
using PRM392.OnlineStore.Application.Interfaces;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Orders.Commands
{
    public class UpdateOrderStatusCommand : IRequest<string>
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderStatusCommand, string>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<string> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindAsync(x => x.OrderId == request.OrderId, cancellationToken);

            if (order == null)
            {
                throw new NotFoundException($"Order with ID {request.OrderId} not found");
            }

            order.OrderStatus = request.Status;
            _orderRepository.Update(order);

            // Save changes to the order
            return await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success" : "Update Failed";
        }
    }
}
