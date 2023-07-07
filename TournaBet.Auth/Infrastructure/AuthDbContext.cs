using Microsoft.EntityFrameworkCore;
using TournaBet.Auth.Infrastructure.EntityConfigurations;
using TournaBet.Shared.Models;

namespace TournaBet.Auth.Infrastructure;

internal class AuthDbContext : DbContext
{
    public DbSet<UserEntity> Users => Set<UserEntity>();

    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
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