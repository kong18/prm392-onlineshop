﻿using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.Create
{
    public class CreateProductCommand : IRequest<string>
    {
        public string Name { get; set; }

        public string? BriefDescription { get; set; }

        public string? FullDescription { get; set; }

        public string? TechnicalSpecifications { get; set; }

        public decimal Price { get; set; }
        public IFormFile? ImageUrl { get; set; }

        public int? CategoryId { get; set; }
    }
}
