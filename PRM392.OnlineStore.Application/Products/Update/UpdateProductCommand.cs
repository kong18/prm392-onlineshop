using MediatR;
using Microsoft.AspNetCore.Http;
namespace PRM392.OnlineStore.Application.Products.Update
{
    public class UpdateProductCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string ?Name { get; set; }

        public string? BriefDescription { get; set; }

        public string? FullDescription { get; set; }

        public string? TechnicalSpecifications { get; set; }

        public decimal? Price { get; set; }
        public IFormFile? ImageUrl { get; set; }

        public int? CategoryId { get; set; }

    }
}
