using System.Net;


namespace Jef.Sdk.Exceptions;
public sealed class JefApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string ResponseBody { get; }

    public JefApiException(
        HttpStatusCode statusCode,
        string responseBody)
        : base($"Jef API request failed with status code {(int)statusCode} ({statusCode}).")
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }
}
