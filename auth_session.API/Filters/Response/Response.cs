using Newtonsoft.Json;

namespace auth_session.API.Filters.Response
{
    public class Response<T>
    {
        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("error")]
        public bool? Error { get; set; }

        [JsonProperty("data")]
        public T? Data { get; set; }

        public Response(T data, string message = "")
        {
            Data = data;
            Message = message;
            Error = false;
        }

        public Response(string error, string message = "")
        {
            Data = default;
            Error = true;
            Message = message == "" ? error : message;
        }
    }
}