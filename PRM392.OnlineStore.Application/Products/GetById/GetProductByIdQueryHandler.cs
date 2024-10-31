using MediatR;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private IProductRepository _productRepository;  
        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var existProduct = await _productRepository.FindAsync(x => x.ProductId == request.ProductId, cancellationToken);
            if(existProduct == null)
            {
                throw new NotFoundException($"Not found Product with id {request.ProductId}");
            }
            return existProduct;    

        }
    }
}
