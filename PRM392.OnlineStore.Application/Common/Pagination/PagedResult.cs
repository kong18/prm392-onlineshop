namespace PRM392.OnlineStore.Application.Common.Pagination
{
    public class PagedResult<T>
    {
        public PagedResult()
        {
            Data = null!;
        }

        public static PagedResult<T> Create(
            int totalCount,
            int pageCount,
            int pageSize,
            int pageNumber,
            IEnumerable<T> data)
        {
            return new PagedResult<T>
            {
                TotalCount = totalCount,
                PageCount = pageCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalPage = (int)Math.Ceiling((double)totalCount / pageSize),
                Data = data,
            };
        }

        public int TotalCount { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int TotalPage { get; set; } // New property for total pages

        public IEnumerable<T> Data { get; set; }
    }
}
