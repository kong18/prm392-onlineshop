using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Carts.Commands
{
    public class CheckoutCartCommandValidator : AbstractValidator<CheckoutCartCommand>
    {
        public CheckoutCartCommandValidator() 
        {
            RuleFor(command => command.LocationId)
                .GreaterThan(0).WithMessage("LocationId must greater than 0");

            RuleFor(command => command.BillingAddress)
                .NotEmpty().WithMessage("BillingAddress can't be empty or null");
        }
    }
}
