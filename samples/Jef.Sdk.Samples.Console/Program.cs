using Jef.Sdk;
using Jef.Sdk.Models;

var apiKey = Environment.GetEnvironmentVariable("JEF_API_KEY")
    ?? throw new InvalidOperationException("Set the JEF_API_KEY environment variable.");

using var client = new JefClient(new JefClientOptions { ApiKey = apiKey });

var response = await client.CreateChatAsync(new ChatRequest
{
    Model = "jef-1",
    Messages = [new ChatMessage("user", "Hello, Jef!")],
});

Console.WriteLine(response.Message.Content);
