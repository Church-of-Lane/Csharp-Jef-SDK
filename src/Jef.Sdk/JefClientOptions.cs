namespace Jef.Sdk;

/// <summary>Configuration for <see cref="JefClient"/>.</summary>
public sealed class JefClientOptions
{
    /// <summary>The configuration section name used by the dependency injection helpers.</summary>
    public const string SectionName = "Jef";

    /// <summary>The API key sent as a bearer token.</summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>The base address of the Jef API.</summary>
    public Uri BaseUrl { get; set; } = new("https://api.jef.example/");

    /// <summary>The timeout applied to each request.</summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);

    internal void Validate()
    {
        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            throw new InvalidOperationException($"{nameof(ApiKey)} must be set.");
        }

        if (!BaseUrl.IsAbsoluteUri)
        {
            throw new InvalidOperationException($"{nameof(BaseUrl)} must be an absolute URI.");
        }

        if (Timeout <= TimeSpan.Zero && Timeout != System.Threading.Timeout.InfiniteTimeSpan)
        {
            throw new InvalidOperationException($"{nameof(Timeout)} must be positive.");
        }
    }

    internal void ConfigureHttpClient(HttpClient httpClient)
    {
        Validate();

        httpClient.BaseAddress = BaseUrl;
        httpClient.Timeout = Timeout;
        httpClient.DefaultRequestHeaders.Authorization = new("Bearer", ApiKey);
    }
}
