namespace PRM392.OnlineStore.Application.Common.Pagination
{
    public class PagedResult<T>
    {
        public PagedResult()
        {
            Data = new List<T>();
        }

        public static PagedResult<T> Create(
            int totalCount,
            int pageCount,
            int pageSize,
            int pageNumber,
            List<T> data)
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
        public int TotalPage { get; set; }
        public List<T> Data { get; set; }  
    }
}
