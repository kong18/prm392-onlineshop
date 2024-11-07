using AutoMapper;
using MediatR;
using PRM392.OnlineStore.Application.Pagination;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.Filter
{
    public class FilterProductQueryHandler : IRequestHandler<FilterProductQuery, PagedResult<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public FilterProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProductDto>> Handle(FilterProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.FindAllAsync(x => x.DeletedAt == null, cancellationToken);

            // Filter products based on search criteria
            if (!string.IsNullOrEmpty(request.ProductName))
            {
                products = products.Where(s => s.ProductName.ToLower().Contains(request.ProductName.ToLower())).ToList();
            }
            if (request.CategoryID.HasValue)
            {
                products = products.Where(s => s.CategoryId == request.CategoryID.Value).ToList();
            }

            // If no products match criteria
            if (!products.Any())
            {
                return new PagedResult<ProductDto>
                {
                    Data = new List<ProductDto>(),
                    TotalCount = 0,
                    PageCount = 0,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };
            }

            // Pagination calculations
            var totalItems = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

            var pagedProducts = products
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            // Map to ProductDto
            var mappedPagedProducts = pagedProducts.MapToProductDTOList(_mapper);

            // Return paged result
            return new PagedResult<ProductDto>
            {
                Data = mappedPagedProducts,
                TotalCount = totalItems,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                PageCount = totalPages
            };
        }
    }
}
