using Banking.UserAccounts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.UserAccounts.Infrastructure.Configurations.Write;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("role_permission");
        
        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });
        
        builder.HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermission)
            .HasForeignKey(rp => rp.RoleId);
        
        builder.HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.PermissionId);
    }
}