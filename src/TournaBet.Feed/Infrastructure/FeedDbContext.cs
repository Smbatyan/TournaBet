using Microsoft.EntityFrameworkCore;

namespace TournaBet.Feed.Infrastructure;

public class FeedDbContext : DbContext
{

    public FeedDbContext(DbContextOptions<FeedDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.ApplyConfiguration(new UsersEntityConfigurations());
    }
}