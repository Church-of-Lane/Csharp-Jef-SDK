using Jef.Sdk.Models;

namespace Jef.Sdk;

public interface IJefClient
{
    Task<Response> EvaluateAsync(Request request);
}
