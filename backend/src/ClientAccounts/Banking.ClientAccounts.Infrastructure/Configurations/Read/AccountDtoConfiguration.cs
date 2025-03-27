using Banking.BankAccounts.Contracts.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.ClientAccounts.Infrastructure.Configurations.Read;

public class AccountDtoConfiguration : IEntityTypeConfiguration<BankAccountDto>
{
    public void Configure(EntityTypeBuilder<BankAccountDto> builder)
    {
        builder.ToTable("accounts");

        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.ClientAccountId);
        
        builder.Property(w => w.PaymentDetails);
        
        builder.Property(w => w.Type);
        
        builder.Property(w => w.Сurrency);
        
        builder.Property(w => w.Balance);
        
        builder.Property(p => p.IsDeleted)
            .HasColumnName("is_deleted");
    }
}