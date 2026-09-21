using System.Net;
using System.Text;
using Jef.Sdk.Exceptions;
using Jef.Sdk.Models;

namespace Jef.Sdk.Tests;

public class JefClientTests
{
    private static readonly ChatRequest Request = new()
    {
        Model = "jef-1",
        Messages = [new ChatMessage("user", "Hello")],
    };

    [Fact]
    public async Task CreateChatAsync_ReturnsDeserializedResponse()
    {
        const string json = """
            {"id":"abc","model":"jef-1","message":{"role":"assistant","content":"Hi"},"usage":{"prompt_tokens":3,"completion_tokens":2}}
            """;
        using var client = CreateClient(HttpStatusCode.OK, json);

        var response = await client.CreateChatAsync(Request);

        Assert.Equal("abc", response.Id);
        Assert.Equal("Hi", response.Message.Content);
        Assert.Equal(5, response.Usage?.TotalTokens);
    }

    [Fact]
    public async Task CreateChatAsync_ThrowsJefApiException_OnErrorStatus()
    {
        using var client = CreateClient(HttpStatusCode.Unauthorized, "bad key");

        var exception = await Assert.ThrowsAsync<JefApiException>(() => client.CreateChatAsync(Request));

        Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
        Assert.Equal("bad key", exception.ResponseBody);
    }

    [Fact]
    public void Constructor_Throws_WhenApiKeyMissing()
    {
        Assert.Throws<InvalidOperationException>(() => new JefClient(new JefClientOptions()));
    }

    private static JefClient CreateClient(HttpStatusCode status, string body) =>
        new(new HttpClient(new StubHandler(status, body)) { BaseAddress = new Uri("https://localhost/") });

    private sealed class StubHandler(HttpStatusCode status, string body) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
            Task.FromResult(new HttpResponseMessage(status)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json"),
            });
    }
}
