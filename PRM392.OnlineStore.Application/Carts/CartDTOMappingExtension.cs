using AutoMapper;
using PRM392.OnlineStore.Application.Products;
using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Carts
{
    public static class CartDTOMappingExtension
    {
        public static CartDTO MapToCartDTO(this Cart projectFrom, IMapper mapper)
          => mapper.Map<CartDTO>(projectFrom);

        public static List<CartDTO> MapToProductDTOList(this IEnumerable<Cart> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToCartDTO(mapper)).ToList();
    }
}
