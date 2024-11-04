using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Categories.Update
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(command => command.ID)
                .NotEmpty().WithMessage("ID can't be null");

            RuleFor(command => command.Name)
                .MaximumLength(100).WithMessage("Name can't be over 100 words");

        }
    }
}
