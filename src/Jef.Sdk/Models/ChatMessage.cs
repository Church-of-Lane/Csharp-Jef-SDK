namespace Jef.Sdk.Models;

/// <summary>A single message in a conversation.</summary>
/// <param name="Role">The author of the message, for example <c>system</c>, <c>user</c> or <c>assistant</c>.</param>
/// <param name="Content">The text of the message.</param>
public sealed record ChatMessage(string Role, string Content);
