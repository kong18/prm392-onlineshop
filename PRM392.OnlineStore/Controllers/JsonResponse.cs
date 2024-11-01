
namespace PRM392.OnlineStore.Api.Controllers
{
    public class JsonResponse<T>
    {
        public JsonResponse(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
    }
}