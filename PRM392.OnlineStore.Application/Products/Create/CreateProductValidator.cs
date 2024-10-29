using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.Create
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator() {
        
        RuleFor(x=> x.Name).NotEmpty().WithMessage("Name can't be null").MaximumLength(100).WithMessage("Name can't be over 100 words");
            RuleFor(command => command.BriefDescription)
                .NotEmpty().WithMessage("BriefDescription can't be empty or null")
                .MaximumLength(60).WithMessage("BriefDescription can't be over 60 words");

            RuleFor(command => command.Price)
               .NotEmpty().WithMessage("Price can't be empty or null")
               .GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(command => command.Price)
               .NotEmpty().WithMessage("Price can't be empty or null")
               .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(command => command.CategoryId)
            .NotEmpty().WithMessage("CategoryID can't be empty or null");
        }
    }
}
