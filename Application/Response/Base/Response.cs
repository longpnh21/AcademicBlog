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
            Message = null;
            Data = data;
        }

        public Response(string error)
        {
            Succeeded = false;
            Message = error;
            Data = default;
        }

        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
        public string Message { get; set; }
    }
}
