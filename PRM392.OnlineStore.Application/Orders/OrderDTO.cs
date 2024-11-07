using AutoMapper;
using PRM392.OnlineStore.Application.Carts;
using PRM392.OnlineStore.Application.Mappings;
using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Orders
{
    public class OrderDTO : IMapFrom<Order>
    {
        public int OrderId { get; set; }

        public string? UserName { get; set; }
        public string? LocationAddress { get; set; }
        public decimal? TotalPrice { get; set; }

        public string PaymentMethod { get; set; } = null!;

        public string BillingAddress { get; set; } = null!;

        public string OrderStatus { get; set; } = null!;

        public string? OrderDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.LocationAddress, opt => opt.MapFrom(src => src.StoreLocation.Address))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Cart.TotalPrice))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate.ToString("dd:MM:yyyy")));
        }
    }
}
