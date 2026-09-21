namespace Jef.Sdk.Models;

/// <summary>A request to generate the next message in a conversation.</summary>
public sealed record ChatRequest
{
    /// <summary>The model to run.</summary>
    public required string Model { get; init; }

    /// <summary>The conversation so far.</summary>
    public required IReadOnlyList<ChatMessage> Messages { get; init; }

    /// <summary>The sampling temperature, or <see langword="null"/> for the server default.</summary>
    public double? Temperature { get; init; }

    /// <summary>The maximum number of tokens to generate, or <see langword="null"/> for the server default.</summary>
    public int? MaxTokens { get; init; }
}
