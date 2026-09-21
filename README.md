# Csharp-Jef-SDK

C# SDK for the Jef model.

## Layout

```
src/Jef.Sdk/                       The SDK (NuGet package Jef.Sdk)
tests/Jef.Sdk.Tests/               xUnit tests
samples/Jef.Sdk.Samples.Console/   Minimal usage example
```

## Usage

```csharp
using var client = new JefClient(new JefClientOptions { ApiKey = "..." });

var response = await client.CreateChatAsync(new ChatRequest
{
    Model = "jef-1",
    Messages = [new ChatMessage("user", "Hello, Jef!")],
});
```

With dependency injection:

```csharp
services.AddJefClient(o => o.ApiKey = configuration["Jef:ApiKey"]!);
```

## Development

```
dotnet build
dotnet test
JEF_API_KEY=... dotnet run --project samples/Jef.Sdk.Samples.Console
```
