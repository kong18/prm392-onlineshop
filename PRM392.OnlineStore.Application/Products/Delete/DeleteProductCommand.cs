using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.Delete
{
    public class DeleteProductCommand : IRequest<string>
    {
        public int ProductId { get; set; }   

    }
}
