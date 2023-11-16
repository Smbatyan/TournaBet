using Microsoft.EntityFrameworkCore;
using TournaBet.Shared.Models;

namespace TournaBet.Auth.Infrastructure;

internal class LigaDbContext : DbContext
{
    public DbSet<PlayerEntity> Users => Set<PlayerEntity>();

    public LigaDbContext(DbContextOptions<LigaDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}