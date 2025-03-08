using Banking.BankAccounts.Domain.Aggregate;
using Banking.BankAccounts.Domain.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.BankAccounts.Infrastructure.Configurations.Write;

public class AccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable("BankAccounts");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).HasConversion(
            id => id.Id,
            value => AccountId.Create(value));
        
        builder.Property(a => a.UserAccountId).IsRequired();
        
        builder.Property(a => a.AccountType).IsRequired();
        
        builder.HasMany(a => a.Wallets).
            WithOne().
            HasForeignKey("account_id").
            OnDelete(DeleteBehavior.Cascade).
            IsRequired();
        
        builder.HasMany(a => a.Cards).
            WithOne().
            HasForeignKey("account_id").
            OnDelete(DeleteBehavior.Cascade).
            IsRequired();

        builder.Property<bool>("_isDeleted").UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}