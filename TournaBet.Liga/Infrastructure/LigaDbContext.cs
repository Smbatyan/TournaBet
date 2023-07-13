using Microsoft.EntityFrameworkCore;
using TournaBet.Auth.Infrastructure.EntityConfigurations;
using TournaBet.Shared.Models;

namespace TournaBet.Auth.Infrastructure;

internal class LigaDbContext : DbContext
{
    public DbSet<UserEntity> Users => Set<UserEntity>();

    public LigaDbContext(DbContextOptions<LigaDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsersEntityConfigurations());
    }
}