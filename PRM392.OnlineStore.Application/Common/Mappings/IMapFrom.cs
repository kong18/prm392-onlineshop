using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Mappings
{
    interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
