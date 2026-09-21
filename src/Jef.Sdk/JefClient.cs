using System.Net.Http.Json;
using System.Text.Json;
using Jef.Sdk.Exceptions;
using Jef.Sdk.Models;

namespace Jef.Sdk;

/// <summary>The default <see cref="IJefClient"/> implementation, backed by an <see cref="HttpClient"/>.</summary>
public sealed class JefClient : IJefClient, IDisposable
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
    };

    private readonly HttpClient _httpClient;
    private readonly bool _ownsHttpClient;

    /// <summary>Initializes a client that creates and owns its own <see cref="HttpClient"/>.</summary>
    /// <param name="options">The client configuration.</param>
    public JefClient(JefClientOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        _httpClient = new HttpClient();
        _ownsHttpClient = true;
        options.ConfigureHttpClient(_httpClient);
    }

    /// <summary>Initializes a client over an already configured <see cref="HttpClient"/>.</summary>
    /// <param name="httpClient">A client with its base address and credentials set.</param>
    public JefClient(HttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);

        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<ChatResponse> CreateChatAsync(ChatRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        using var response = await _httpClient
            .PostAsJsonAsync("v1/chat", request, JsonOptions, cancellationToken)
            .ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            throw new JefApiException(response.StatusCode, body);
        }

        return await response.Content
            .ReadFromJsonAsync<ChatResponse>(JsonOptions, cancellationToken)
            .ConfigureAwait(false)
            ?? throw new JefApiException(response.StatusCode, "The response body was empty.");
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_ownsHttpClient)
        {
            _httpClient.Dispose();
        }
    }
}
