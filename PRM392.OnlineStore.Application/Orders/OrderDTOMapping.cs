using AutoMapper;
using PRM392.OnlineStore.Application.Carts;
using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Orders
{
    public static class OrderDTOMapping
    {
        public static OrderDTO MapToOrderDTO(this Order projectFrom, IMapper mapper)
          => mapper.Map<OrderDTO>(projectFrom);

        public static List<OrderDTO> MapToOrderDTOList(this IEnumerable<Order> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToOrderDTO(mapper)).ToList();
    }
}
