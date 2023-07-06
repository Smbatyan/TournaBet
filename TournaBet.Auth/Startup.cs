using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TournaBet.Auth.Extensions;
using TournaBet.Auth.Options;
using TournaBet.Auth.Services;

namespace TournaBet.Auth;

public class Startup : TournaBet.Shared.IStartup
{
    private readonly IConfiguration _config;

    public Startup()
    {
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("auth.appsettings.json", optional: true)
            .AddEnvironmentVariables();

        _config = configBuilder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<AuthOptions>(_config.GetSection("Jwt"));
        services.AddAsymmetricJwtAuthentication();

        services.AddScoped<TokenService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseEndpoints(endpoints =>
            endpoints.MapGet("/test",
                async context => { await context.Response.WriteAsync("Hello World from auth"); }).RequireAuthorization()
        );

        app.UseAuthentication();
        app.UseAuthorization();
    }
}