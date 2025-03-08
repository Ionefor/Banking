using Banking.BankAccounts.Contracts.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.BankAccounts.Infrastructure.Configurations.Read;

public class CardDtoConfiguration : IEntityTypeConfiguration<CardDto>
{
    public void Configure(EntityTypeBuilder<CardDto> builder)
    {
        builder.ToTable("Cards");

        builder.HasKey(c => c.Id);
        
        builder.HasOne<WalletDto>() 
            .WithMany() 
            .HasForeignKey(c => c.WalletId); 
        
        builder.Property(c => c.PaymentDetails);
        
        builder.Property(c => c.Ccv);
        
        builder.Property(c => c.ValidThru);
 
        builder.Property(c => c.IsDeleted)
            .HasColumnName("is_deleted");
    }
}