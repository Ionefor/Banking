using Banking.Accounts.Contracts.Dto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.Accounts.Infrastructure.Configurations.Read;

public class CorporateAccountDtoConfiguration : IEntityTypeConfiguration<CorporateAccountDto>
{
    public void Configure(EntityTypeBuilder<CorporateAccountDto> builder)
    {
        builder.ToTable("corporate_accounts");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.UserId);

        builder.Property(c => c.ContactPhone);
        
        builder.ComplexProperty(c => c.Address, ca =>
        {
            ca.Property(c => c.Country).
                HasColumnName("country");

            ca.Property(c => c.City).
                HasColumnName("city");

            ca.Property(c => c.Street).
                HasColumnName("street");

            ca.Property(c => c.HouseNumber).
                HasColumnName("house_number");
        });
        
        builder.Property(c => c.ContactEmail);
        
        builder.Property(c => c.CompanyName);
        
        builder.Property(c => c.TaxId);
    }
}