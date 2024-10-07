using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Domain.Entities.Repositories
{
    public interface IPagedResult<out T> : IEnumerable<T>
    {
        int TotalCount { get; }
        int PageCount { get; }
        int PageNo { get; }
        int PageSize { get; }
    }
}
