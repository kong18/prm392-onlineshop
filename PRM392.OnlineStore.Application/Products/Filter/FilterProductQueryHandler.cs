using MediatR;
using PRM392.OnlineStore.Application.Pagination;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.Filter
{
    public class FilterProductQueryHandler : IRequestHandler<FilterProductQuery, PagedResult<Product>>
    {
        private readonly IProductRepository _productRepository;

        public FilterProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResult<Product>> Handle(FilterProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.FindAllWithCategoriesAsync(cancellationToken);
            if (!string.IsNullOrEmpty(request.ProductName))
            {
                products = products.Where(s => s.ProductName.ToLower().Contains(request.ProductName.ToLower())).ToList();
            }
            if (request.CategoryID.HasValue)
            {
                products = products.Where(s => s.CategoryId == request.CategoryID.Value).ToList();
            }

            var totalItems = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);
            var pagedProducts = products
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

           
            var result = new PagedResult<Product>
            {
                Data = pagedProducts,
                TotalCount = totalItems,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPage = totalPages // Set total pages
            };

            return result;
        }
    }
}
