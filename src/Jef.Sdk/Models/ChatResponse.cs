namespace Jef.Sdk.Models;

/// <summary>The model's reply to a <see cref="ChatRequest"/>.</summary>
public sealed record ChatResponse
{
    /// <summary>The unique identifier of the response.</summary>
    public required string Id { get; init; }

    /// <summary>The model that produced the response.</summary>
    public required string Model { get; init; }

    /// <summary>The generated message.</summary>
    public required ChatMessage Message { get; init; }

    /// <summary>Token accounting for the request, if reported.</summary>
    public Usage? Usage { get; init; }
}

/// <summary>Token counts for a request.</summary>
/// <param name="PromptTokens">Tokens in the input.</param>
/// <param name="CompletionTokens">Tokens in the generated output.</param>
public sealed record Usage(int PromptTokens, int CompletionTokens)
{
    /// <summary>The combined token count.</summary>
    public int TotalTokens => PromptTokens + CompletionTokens;
}
