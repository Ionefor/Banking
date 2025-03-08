using Banking.BankAccounts.Domain.Entities;
using Banking.BankAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.BankAccounts.Infrastructure.Configurations.Write;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id).HasConversion(
            id => id.Id,
            value => WalletId.Create(value));
        
        builder.Property(w => w.Type).IsRequired();
        
        builder.Property(w => w.Сurrency).IsRequired();
        
        builder.ComplexProperty(w => w.PaymentDetails,
            pb =>
            {
                pb.Property(d => d.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("paymentDetails");
            });
        
        builder.ComplexProperty(w => w.Balance,
            bb =>
            {
                bb.Property(b => b.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("balance");
            });

        builder.Property<bool>("_isDeleted").UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}