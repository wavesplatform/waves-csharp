using System.Net;

namespace Waves.NET.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; init; }

        public HttpException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
