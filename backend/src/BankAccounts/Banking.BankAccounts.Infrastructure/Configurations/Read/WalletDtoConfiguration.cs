using Banking.BankAccounts.Contracts.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.BankAccounts.Infrastructure.Configurations.Read;

public class WalletDtoConfiguration : IEntityTypeConfiguration<WalletDto>
{
    public void Configure(EntityTypeBuilder<WalletDto> builder)
    {
        builder.ToTable("Wallets");

        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.PaymentDetails);
        
        builder.Property(w => w.Type);
        
        builder.Property(w => w.Сurrency);
        
        builder.Property(w => w.Balance);
        
        builder.Property(p => p.IsDeleted)
            .HasColumnName("is_deleted");
    }
}