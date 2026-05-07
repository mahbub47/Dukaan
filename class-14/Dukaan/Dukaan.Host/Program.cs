using Dukaan.Application;
using Dukaan.Infrastructure;
using Dukaan.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Service Registration Section ---
// This is where we register dependencies for the built-in Dependency Injection (DI) container

// Register infrastructure services (like DbContext, Identity, repositories) from the Infrastructure project
builder.Services.AddInfrastructure(builder.Configuration);

// Register application services (like TenantService, AuthService) from the Application project
builder.Services.AddApplication();

// Register OpenAPI (Swagger) for API documentation
builder.Services.AddOpenApi();

// Register MVC controllers
builder.Services.AddControllers();

var app = builder.Build();

// --- 2. Middleware Pipeline Section ---
// This defines the order in which HTTP requests are processed.

if (app.Environment.IsDevelopment())
{
    // Enables the interactive Swagger UI in development mode
    app.MapOpenApi();
}

// Redirects HTTP requests to HTTPS
app.UseHttpsRedirection();

// Reads the incoming request, validates the authentication token (like JWT or cookie), and sets the user identity
app.UseAuthentication();

// Maps controller routes (e.g., [Route("api/[controller]")])
app.MapControllers();

// Starts the application
app.Run();