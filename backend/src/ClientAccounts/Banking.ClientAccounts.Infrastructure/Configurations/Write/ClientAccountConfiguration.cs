using Banking.ClientAccounts.Domain.Aggregate;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.ClientAccounts.Infrastructure.Configurations.Write;

public class ClientAccountConfiguration : IEntityTypeConfiguration<ClientAccount>
{
    public void Configure(EntityTypeBuilder<ClientAccount> builder)
    {
        builder.ToTable("client_accounts");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).HasConversion(
            id => id.Id,
            value => ClientAccountId.Create(value));
        
        builder.Property(a => a.UserAccountId).IsRequired().
            HasColumnName("user_account_id");
        
        builder.Property(a => a.UserAccountType).IsRequired();
        
        builder.HasMany(a => a.Accounts).
            WithOne().
            HasForeignKey("client_account_id").
            OnDelete(DeleteBehavior.Cascade).
            IsRequired();
        
        builder.HasMany(a => a.Cards).
            WithOne().
            HasForeignKey("client_account_id").
            OnDelete(DeleteBehavior.Cascade).
            IsRequired();

        builder.Property<bool>("IsDeleted").
            UsePropertyAccessMode(PropertyAccessMode.Field).
            HasColumnName("is_deleted");
    }
}