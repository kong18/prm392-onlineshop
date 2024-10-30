using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace PRM392.OnlineStore.Application.Products.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(command => command.BriefDescription)
                .MaximumLength(255).WithMessage("Brief description cannot exceed 255 characters.");

            RuleFor(command => command.FullDescription)
                .MaximumLength(1000).WithMessage("Full description cannot exceed 1000 characters.");

            RuleFor(command => command.TechnicalSpecifications)
                .MaximumLength(500).WithMessage("Technical specifications cannot exceed 500 characters.");

            RuleFor(command => command.Price)
                .GreaterThan(0).When(command => command.Price.HasValue)
                .WithMessage("Price must be a positive value.");

            RuleFor(command => command.ImageUrl)
                .Must(IsValidImageFormat).When(command => command.ImageUrl != null)
                .WithMessage("Image must be a valid image file format (jpeg, png, gif).");

            RuleFor(command => command.CategoryId)
                .GreaterThan(0).When(command => command.CategoryId.HasValue)
                .WithMessage("Category ID must be greater than 0.");
        }

        private bool IsValidImageFormat(IFormFile? file)
        {
            if (file == null) return false;
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            return validExtensions.Contains(fileExtension);
        }
    }
}
