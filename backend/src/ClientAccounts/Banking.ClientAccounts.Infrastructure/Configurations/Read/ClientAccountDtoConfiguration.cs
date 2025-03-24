using Banking.BankAccounts.Contracts.Dto;
using Banking.ClientAccounts.Domain.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.ClientAccounts.Infrastructure.Configurations.Read;

public class  ClientAccountDtoConfiguration : IEntityTypeConfiguration<ClientAccountDto>
{
    public void Configure(EntityTypeBuilder<ClientAccountDto> builder)
    {
        builder.ToTable("client_accounts");

        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.AccountId);
        
        builder.Property(a => a.UserAccountType);
        
        builder.HasMany(a => a.Accounts).
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