using Dukaan.Application.Interfaces;
using Dukaan.Infrastructure.Data.DbContext;
using Dukaan.Infrastructure.Data.Model;
using Dukaan.Infrastructure.Data.Repositories;
using Dukaan.Infrastructure.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Dukaan.Infrastructure;

/// <summary>
/// Provides extension methods for registering infrastructure services and dependencies in the application's dependency
/// injection container.
/// </summary>
/// <remarks>This class is intended to be used in the application's startup configuration to add
/// infrastructure-related services, such as database contexts, identity management, and repositories, to the service
/// collection. It centralizes the registration of these services to promote modularity and maintainability.</remarks>
public static class DependencyInjection
{

    /// <summary>
    /// Adds infrastructure services to the application's dependency injection container, including database context,
    /// identity, and repository services.
    /// </summary>
    /// <remarks>This method configures Entity Framework Core with PostgreSQL, sets up ASP.NET Core Identity
    /// for authentication, and registers user and repository services required for the application's infrastructure
    /// layer.</remarks>
    /// <param name="services">The service collection to which the infrastructure services will be added.</param>
    /// <param name="configuration">The application configuration used to retrieve settings such as the database connection string.</param>
    /// <returns>The same service collection instance with infrastructure services registered. This enables method chaining.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // Register the Database Context with PostgreSQL support
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        // Register ASP.NET Core Identity for authentication
        services.AddIdentity<Merchant, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // Register application services
        services.AddScoped<IUserService, UserService>();

        // Register the generic repository for dependency injection
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Registers the generic repository

        // Default authentication scheme and JWT authentication configuration
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });

        return services;
    }
}

