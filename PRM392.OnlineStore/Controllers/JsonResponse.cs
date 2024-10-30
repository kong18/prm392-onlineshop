
namespace PRM392.OnlineStore.Api.Controllers
{
    public class JsonResponse<T>
    {
        public JsonResponse(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}