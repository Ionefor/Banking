using Banking.SharedKernel.Definitions;
using Banking.UserAccounts.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.UserAccounts.Infrastructure.Configuration.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<IdentityUserRole<Guid>>();
        
        builder.ComplexProperty(v => v.FullName, fb =>
        {
            fb.Property(f => f.FirstName)
                .IsRequired()
                .HasMaxLength(Constants.Shared.MaxLowTextLength)
                .HasColumnName("firstName");

            fb.Property(f => f.MiddleName)
                .IsRequired()
                .HasMaxLength(Constants.Shared.MaxLowTextLength)
                .HasColumnName("middleName");

            fb.Property(f => f.LastName)
                .IsRequired(false)
                .HasMaxLength(Constants.Shared.MaxLowTextLength)
                .HasColumnName("lastName");
        });
    }
}