using MediatR;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, string>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<string> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var existProduct = await  _productRepository.FindAsync(x => x.ProductId == request.ProductId);
            if (existProduct == null)
            {
                throw new NotFoundException("Product does not exist");
            }
            existProduct.DeletedAt = DateTime.UtcNow;
            _productRepository.Update(existProduct);
            return await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Delete Success!" : "Delete Fail!";

        }
    }
}
