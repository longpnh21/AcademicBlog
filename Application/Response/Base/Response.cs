using System.Net;

namespace Application.Response.Base
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }

        public Response(string error)
        {
            Succeeded = false;
            Message = error;
            Errors = null;
            Data = default;
        }

        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
