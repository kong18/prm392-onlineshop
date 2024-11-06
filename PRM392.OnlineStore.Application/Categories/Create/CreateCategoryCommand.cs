using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Categories.Create
{
    public class CreateCategoryCommand : IRequest<string>
    {
        public string CategoryName { get; set; }
    }
}
