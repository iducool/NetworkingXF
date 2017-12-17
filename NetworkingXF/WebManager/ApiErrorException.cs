using System;
using System.Net;

namespace NetworkingXF.WebManager
{
    public class ApiErrorException : Exception
    {
        public HttpStatusCode? HttpStatusCode { get; }

        public string ReasonPhrase { get; }

        public ApiErrorException(HttpStatusCode statusCode, string reasonPhrase)
        {
            HttpStatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
        }
    }
}
