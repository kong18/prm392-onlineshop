using AutoMapper;
using PRM392.OnlineStore.Application.Products;
using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Categories
{
    public static class CategoryMappingExtension 
    {
        public static CategoryDTO MapToCategoryDTO(this Category projectFrom, IMapper mapper)
         => mapper.Map<CategoryDTO>(projectFrom);

        public static List<CategoryDTO> MapToCategoryDTOList(this IEnumerable<Category> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToCategoryDTO(mapper)).ToList();
    }
}
