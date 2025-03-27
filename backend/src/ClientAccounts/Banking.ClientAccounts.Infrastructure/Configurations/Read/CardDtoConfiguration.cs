using Banking.BankAccounts.Contracts.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.ClientAccounts.Infrastructure.Configurations.Read;

public class CardDtoConfiguration : IEntityTypeConfiguration<CardDto>
{
    public void Configure(EntityTypeBuilder<CardDto> builder)
    {
        builder.ToTable("cards");

        builder.HasKey(c => c.Id);
        
        builder.HasOne<BankAccountDto>() 
            .WithMany() 
            .HasForeignKey(c => c.AccountId); 
        
        builder.Property(c => c.ClientAccountId);
        
        builder.Property(c => c.PaymentDetails);
        
        builder.Property(c => c.IsMain);
        
        builder.Property(c => c.Ccv);
        
        builder.Property(c => c.ValidThru);
 
        builder.Property(c => c.IsDeleted)
            .HasColumnName("is_deleted");
    }
}