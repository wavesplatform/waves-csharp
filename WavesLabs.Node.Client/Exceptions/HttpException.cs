using System.Net;

namespace WavesLabs.Node.Client.Exceptions
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
