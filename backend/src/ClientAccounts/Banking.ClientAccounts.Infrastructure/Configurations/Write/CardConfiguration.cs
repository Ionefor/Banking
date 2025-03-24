using Banking.ClientAccounts.Domain.Entities;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.ClientAccounts.Infrastructure.Configurations.Write;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("cards");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasConversion(
            id => id.Id,
            value => CardId.Create(value));
        
        builder.ComplexProperty(c => c.PaymentDetails,
            pb =>
            {
                pb.Property(d => d.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("payment_details");
            });
        
        builder.ComplexProperty(c => c.Ccv,
            cb =>
            {
                cb.Property(c => c.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("ccv");
            });
        
        builder.HasOne<Account>()
            .WithMany() 
            .HasForeignKey(c => c.AccountId) 
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(c => c.ValidThru).
            IsRequired().HasColumnName("valid_thru");
        
        builder.Property(c => c.IsMain).
            IsRequired().HasColumnName("is_main");
        
        builder.Property<bool>("IsDeleted").
            UsePropertyAccessMode(PropertyAccessMode.Field).
            HasColumnName("is_deleted");
    }
}