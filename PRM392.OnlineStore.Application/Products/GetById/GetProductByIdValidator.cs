using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.GetById
{
    public class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Id must be filled in")
                .GreaterThan(0).WithMessage("Id must be a positive integer");
        }

      
    }
}
