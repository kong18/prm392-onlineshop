using MediatR;
using PRM392.OnlineStore.Application.Products;
using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Categories.Filter
{
    public class FilterCategoryQuery : IRequest<List<CategoryDTO>>
    {
        public string? CategoryName { get; set; }

        public FilterCategoryQuery(string name) {
        CategoryName = name;
        }
    }
}
