using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Orders.Commands
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(command => command.ProductId)
                .GreaterThan(0).WithMessage("ProductId must greater than 0");

            RuleFor(command => command.Quantity)
                .GreaterThan(0).WithMessage("Quantity must greater than 0");

            RuleFor(command => command.LocationId)
                .GreaterThan(0).WithMessage("LocationId must greater than 0");

            RuleFor(command => command.BillingAddress)
                .NotEmpty().WithMessage("BillingAddress can't be empty or null");
        }
    }
}
