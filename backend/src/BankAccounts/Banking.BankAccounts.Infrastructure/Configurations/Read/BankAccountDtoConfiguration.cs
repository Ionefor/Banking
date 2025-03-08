using Banking.BankAccounts.Contracts.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.BankAccounts.Infrastructure.Configurations.Read;

public class BankAccountDtoConfiguration : IEntityTypeConfiguration<BankAccountDto>
{
    public void Configure(EntityTypeBuilder<BankAccountDto> builder)
    {
        builder.ToTable("BankAccounts");

        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.UserAccountId);
        
        builder.Property(a => a.AccountType);
        
        builder.HasMany(a => a.Wallets).
            WithOne().
            HasForeignKey(w => w.Id).
            IsRequired();
        
        builder.HasMany(a => a.Cards).
            WithOne().
            HasForeignKey(c => c.Id).
            IsRequired();
        
        builder.Property(a => a.IsDeleted)
            .HasColumnName("is_deleted");
    }
}