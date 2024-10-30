using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.Delete
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>    
    {
        public DeleteProductCommandValidator() {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product can't not be null");      
        }  
    }
}
