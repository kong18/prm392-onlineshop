using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Categories.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(command => command.CategoryName)
                .NotEmpty().WithMessage("Name can't be empty or null")
                .MaximumLength(100).WithMessage("Name can't be over 100 words");

        }
    }
}
