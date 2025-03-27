using Banking.ClientAccounts.Domain.Entities;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.ClientAccounts.Infrastructure.Configurations.Write;

public class AccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable("accounts");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id).HasConversion(
            id => id.Id,
            value => BankAccountId.Create(value));
        
        builder.Property(w => w.Type).IsRequired();
        
        builder.Property(w => w.Сurrency).IsRequired();
        
        builder.ComplexProperty(w => w.PaymentDetails,
            pb =>
            {
                pb.Property(d => d.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("payment_details");
            });
        
        builder.ComplexProperty(w => w.Balance,
            bb =>
            {
                bb.Property(b => b.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("balance");
            });

        builder.Property<bool>("IsDeleted").
            UsePropertyAccessMode(PropertyAccessMode.Field).
            HasColumnName("is_deleted");
    }
}