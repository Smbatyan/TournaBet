using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TournaBet.Auth.Infrastructure;

internal class LigaDbContextFactory: IDesignTimeDbContextFactory<LigaDbContext>
{
    public LigaDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("auth.appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<LigaDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("db"));

        return new LigaDbContext(optionsBuilder.Options);
    }
}