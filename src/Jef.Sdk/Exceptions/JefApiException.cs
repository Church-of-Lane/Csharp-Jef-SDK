using System.Net;

namespace Jef.Sdk.Exceptions;

/// <summary>Thrown when the Jef API returns a non-success status code.</summary>
public sealed class JefApiException : Exception
{
    /// <summary>Initializes a new instance of the <see cref="JefApiException"/> class.</summary>
    /// <param name="statusCode">The HTTP status code returned by the API.</param>
    /// <param name="responseBody">The raw response body, if any.</param>
    public JefApiException(HttpStatusCode statusCode, string? responseBody)
        : base($"The Jef API returned {(int)statusCode} ({statusCode}).")
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }

    /// <summary>The HTTP status code returned by the API.</summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>The raw response body, if any.</summary>
    public string? ResponseBody { get; }
}
