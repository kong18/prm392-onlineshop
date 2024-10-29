﻿using MediatR;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IProductRepository _productRepository; 
       private readonly ICategoryRepository _categoryRepository;
        public CreateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
    
        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var categoryExist = _categoryRepository.FindAsync(x => x.CategoryId == request.CategoryId);
            if (categoryExist is null)
            {
                throw new NotFoundException("Category does not exist");
            }

            bool productExist = await _productRepository.AnyAsync(x=> x.ProductName == request.Name && !x.DeletedAt.HasValue, cancellationToken);
            if (productExist)
            {
                throw new DuplicationException("Same product has been existed");
            }
            var p = new Product
            {
                ProductId = request.Id,
                CategoryId = categoryExist.Id,
                BriefDescription = request.BriefDescription,
                FullDescription = request.FullDescription,
                ProductName = request.Name,
                CreatedAt = DateTime.UtcNow.AddHours(7)
            };

            _productRepository.Add(p);
             return await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create Success!" : "Create Fail!";
        }
    }
}