using System.Net;
using System.Text.Json;

namespace BlogApi.ReadModel
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public string Message { get; set; } = "An unexpected error occured.";
        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
