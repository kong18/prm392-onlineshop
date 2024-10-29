using AutoMapper;
using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products
{
    public static class ProductMappingExtension
    {
        public static ProductDto MapToProductDTO(this Product projectFrom, IMapper mapper)
          => mapper.Map<ProductDto>(projectFrom);

        public static List<ProductDto> MapToProductDTOList(this IEnumerable<Product> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToProductDTO(mapper)).ToList();
    }
}
