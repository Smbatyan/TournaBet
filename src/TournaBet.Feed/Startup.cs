using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TournaBet.Feed.Configurations.Settings;
using TournaBet.Feed.Infrastructure;

namespace TournaBet.Feed;

public class Startup : TournaBet.Shared.IStartup
{
    private readonly IConfiguration _config;

    public Startup()
    {
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("feed.appsettings.json", optional: true)
            .AddEnvironmentVariables();

        _config = configBuilder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();

        // Add the IOptions<FeedSettings> service.
        services.AddOptions<FeedApiSettings>();

        // Add the BaseClientService and FeedService services.
        // services.AddSingleton<BaseClientService>();
        // services.AddSingleton<FeedService>();
        
        services.AddDbContext<FeedDbContext>(options =>
            options.UseNpgsql(_config.GetConnectionString("db")));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseEndpoints(endpoints =>
            endpoints.MapGet("/test",
                async context => { await context.Response.WriteAsync("Hello World from auth"); })
        );

        app.UseAuthentication();
        app.UseAuthorization();
    }
}