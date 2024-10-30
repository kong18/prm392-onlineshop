using MediatR;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.CartItems.Commands.Create
{
    public record CreateCartItemCommand : IRequest<string>
    {
        public string ProductID { get; init; }
        public int Quantity { get; init; }
    }
    //public class CreateCartItemCommandHandler
    //{
    //    private readonly IProductRepository productRepository;
    //    private readonly 
    //}
}
