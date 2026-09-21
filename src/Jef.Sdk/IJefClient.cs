using Jef.Sdk.Models;

namespace Jef.Sdk;

/// <summary>A client for the Jef API.</summary>
public interface IJefClient
{
    /// <summary>Generates the next message in a conversation.</summary>
    /// <param name="request">The conversation and generation settings.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <exception cref="Exceptions.JefApiException">The API returned a non-success status code.</exception>
    Task<ChatResponse> CreateChatAsync(ChatRequest request, CancellationToken cancellationToken = default);
}
