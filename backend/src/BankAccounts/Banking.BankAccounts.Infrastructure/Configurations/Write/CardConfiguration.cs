using Banking.BankAccounts.Domain.Entities;
using Banking.BankAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.BankAccounts.Infrastructure.Configurations.Write;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Cards");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasConversion(
            id => id.Id,
            value => CardId.Create(value));
        
        builder.ComplexProperty(c => c.PaymentDetails,
            pb =>
            {
                pb.Property(d => d.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("paymentDetails");
            });
        
        builder.ComplexProperty(c => c.Ccv,
            cb =>
            {
                cb.Property(c => c.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("ccv");
            });
        
        builder.HasOne<Wallet>()
            .WithMany() 
            .HasForeignKey(c => c.WalletId) 
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(c => c.ValidThru).
            IsRequired().HasColumnName("validThru");
        
        builder.Property<bool>("_isDeleted").UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}