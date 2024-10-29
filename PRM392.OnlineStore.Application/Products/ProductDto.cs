﻿using AutoMapper;
using PRM392.OnlineStore.Application.Mappings;
using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products
{
    public class ProductDto : IMapFrom<Product>
    {
        public int Id {  get; set; } 
        public string Name { get; set; }

        public string? BriefDescription { get; set; }

        public string? FullDescription { get; set; }

        public string? TechnicalSpecifications { get; set; }

        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>().ForMember(dto => dto.CategoryName, opt => opt.MapFrom(entity => entity.Category.CategoryName));         
        }
    }
}