using MediatR;
using PRM392.OnlineStore.Application.FileUpload;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly FileUploadService _fileUploadService;

        public UpdateProductCommandHandler(FileUploadService fileUploadService,IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _fileUploadService = fileUploadService;
        }

        public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existProduct = await _productRepository.FindAsync(x => x.ProductId == request.Id, cancellationToken);
            if (existProduct is null || existProduct.DeletedAt.HasValue) { throw new NotFoundException("Product cant'be found"); }
            var category = await _categoryRepository.FindAsync(x => x.CategoryId == request.Id, cancellationToken);
            if (category == null) { throw new NotFoundException("Category cant'be found"); }

            string imageUrl = string.Empty;
            if (request.ImageUrl != null)
            {
                using (var stream = request.ImageUrl.OpenReadStream())
                {
                    imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
                }
            }
            existProduct.ProductName = request.Name ?? existProduct.ProductName;
            existProduct.Price = request.Price ?? existProduct.Price;
            existProduct.CategoryId = request.CategoryId ?? existProduct.CategoryId;
            existProduct.ImageUrl = !string.IsNullOrEmpty(imageUrl) ? imageUrl : existProduct.ImageUrl;
            existProduct.UpdatedAt = DateTime.Now;  
            _productRepository.Update(existProduct);
            return await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success!" : "Update Fail!";
        }
    }
}
