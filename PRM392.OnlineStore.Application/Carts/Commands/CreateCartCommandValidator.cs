using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Carts.Commands
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator() 
        {
            RuleFor(command => command.ProductId)
                .GreaterThan(0).WithMessage("ProductId must greater than 0");

            RuleFor(command => command.Quantity)
                .GreaterThan(0).WithMessage("Quantity must greater than 0");
        }
    }
}
