using System.Net;

namespace Blocks.Exceptions;

public class HttpException : Exception
{
    public HttpException(HttpStatusCode statusCode, string message) : base(string.IsNullOrEmpty(message)
        ? statusCode.ToString()
        : message)
    {
        this.StatusCode = statusCode;
    }

    public HttpException(HttpStatusCode statusCode, string message, Exception exception) : base(message, exception)
    {
        this.StatusCode = statusCode;
    }


    public HttpStatusCode StatusCode { get; }
    
    
    
}