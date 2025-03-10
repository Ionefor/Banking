using Banking.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.Users.Infrastructure.Configurations.Write;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasIndex(p => p.Code)
            .IsUnique();
    }
}