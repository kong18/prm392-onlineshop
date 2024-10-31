using MediatR;
using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.GetById
{
    public  class GetProductByIdQuery : IRequest<Product>
    {
        public int ProductId { get; set; }   
        public GetProductByIdQuery(int Id) {
        ProductId = Id; 
        }

    }
}
