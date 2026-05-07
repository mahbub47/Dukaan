using Dukaan.Application.Interfaces;
using Dukaan.Application.Services;
using Dukaan.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dukaan.Application;

/// <summary>
/// Provides extension methods for registering application services with the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds application-level services to the specified service collection.
    /// </summary>
    /// <remarks>This method registers application services such as <see cref="TenantService"/> and <see
    /// cref="IAuthService"/> with scoped lifetimes. Call this method during application startup to ensure required
    /// services are available for dependency injection.</remarks>
    /// <param name="services">The service collection to which the application services will be added. Cannot be null.</param>
    /// <returns>The same instance of <see cref="IServiceCollection"/> that was provided, to support method chaining.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register application services with scoped lifetimes
        services.AddScoped<TenantService>();

        // Register the authentication service interface and its implementation
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
