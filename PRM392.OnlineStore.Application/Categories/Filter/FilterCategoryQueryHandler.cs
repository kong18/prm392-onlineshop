using AutoMapper;
using MediatR;
using PRM392.OnlineStore.Application.Products;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Categories.Filter
{
    public class FilterCategoryQueryHandler : IRequestHandler<FilterCategoryQuery, List<CategoryDTO>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public FilterCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<List<CategoryDTO>> Handle(FilterCategoryQuery request, CancellationToken cancellationToken)
        {
            var query = await _categoryRepository.FindAllAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.CategoryName))
            {
                query = query.Where(b => b.CategoryName.Contains(request.CategoryName)).ToList();
            }

            return query.MapToCategoryDTOList(_mapper); 

        }
    }
}
