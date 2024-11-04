using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Categories.Update
{
    public class UpdateCategoryCommand : IRequest<string>
    {
        public UpdateCategoryCommand() { }
        public UpdateCategoryCommand(int id,string name)
        {
            ID = id;
         
            Name = name;
        }

        public int ID { get; set; }
        public string? Name { get; set; }
    }
}
