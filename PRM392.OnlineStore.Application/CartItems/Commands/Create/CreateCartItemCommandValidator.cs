using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.CartItems.Commands.Create
{
    public class CreateCartItemCommandValidator : AbstractValidator<CreateCartItemCommand>
    {
        public CreateCartItemCommandValidator()
        {
            RuleFor(x => x.ProductID)
            .NotEmpty().WithMessage("ProductID cannot be null or empty.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}
