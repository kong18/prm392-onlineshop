using MediatR;
using PRM392.OnlineStore.Application.Common.Pagination;
using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Products.Filter
{
    public class FilterProductQuery : IRequest<PagedResult< Product>>
    {
        public string? ProductName {  get; set; }
        public int? CategoryID { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
