using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.Create
{
    public class CreateProductCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? BriefDescription { get; set; }

        public string? FullDescription { get; set; }

        public string? TechnicalSpecifications { get; set; }

        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        public int? CategoryId { get; set; }
    }
}
