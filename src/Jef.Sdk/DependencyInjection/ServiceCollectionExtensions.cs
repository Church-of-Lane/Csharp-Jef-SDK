using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Jef.Sdk.DependencyInjection;

/// <summary>Dependency injection helpers for <see cref="IJefClient"/>.</summary>
public static class ServiceCollectionExtensions
{
    /// <summary>Registers <see cref="IJefClient"/> as a typed <see cref="HttpClient"/>.</summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">Configures <see cref="JefClientOptions"/>.</param>
    /// <returns>The <see cref="IHttpClientBuilder"/>, for adding handlers such as retry policies.</returns>
    public static IHttpClientBuilder AddJefClient(this IServiceCollection services, Action<JefClientOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        services.Configure(configure);

        return services.AddHttpClient<IJefClient, JefClient>((serviceProvider, httpClient) =>
            serviceProvider.GetRequiredService<IOptions<JefClientOptions>>().Value.ConfigureHttpClient(httpClient));
    }
}
