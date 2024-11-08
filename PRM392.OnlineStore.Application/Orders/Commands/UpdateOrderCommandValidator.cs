using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Orders.Commands
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        private readonly string[] _validStatuses = { "Pending", "Success", "Cancelled", "Processing" }; // Define valid statuses

        public UpdateOrderCommandValidator()
        {
            RuleFor(command => command.OrderId)
                .GreaterThan(0).WithMessage("OrderId must greater than 0");

            RuleFor(command => command.Status)
                .NotEmpty().WithMessage("Status can't be empty or null")
                .Must(BeAValidStatus).WithMessage("Invalid order status. Status must be one of these: Pending, Success, Cancelled, Processing");
        }

        private bool BeAValidStatus(string status)
        {
            return _validStatuses.Contains(status);
        }
    }
}
