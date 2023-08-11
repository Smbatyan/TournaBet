using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TournaBet.Shared.Models;

namespace TournaBet.Auth.Infrastructure.EntityConfigurations;

public class UsersEntityConfigurations : IEntityTypeConfiguration<PlayerEntity>
{
    public void Configure(EntityTypeBuilder<PlayerEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Username).IsRequired();
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("now() at time zone 'utc'");
        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("now() at time zone 'utc'");
    }
}