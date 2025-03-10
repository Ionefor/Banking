using Banking.Accounts.Contracts.Dto.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.Accounts.Infrastructure.Configurations.Read;


public class IndividualAccountDtoConfiguration : IEntityTypeConfiguration<IndividualAccountDto>
{
    public void Configure(EntityTypeBuilder<IndividualAccountDto> builder)
    {
        builder.ToTable("individual_accounts");

        builder.HasKey(i => i.Id);
        
        builder.Property(i => i.UserId);

        builder.Property(i => i.PhoneNumber);

        builder.ComplexProperty(i => i.FullName, ifb =>
        {
            ifb.Property(f => f.FirstName).
                HasColumnName("firstName");

            ifb.Property(m => m.MiddleName).
                HasColumnName("middleName");

            ifb.Property(l => l.LastName).
                HasColumnName("lastName");
        });
        
        builder.ComplexProperty(i => i.Address, ia =>
        {
            ia.Property(c => c.Country).
                HasColumnName("country");

            ia.Property(c => c.City).
                HasColumnName("city");

            ia.Property(c => c.Street).
                HasColumnName("street");

            ia.Property(c => c.HouseNumber).
                HasColumnName("house_number");
        });
        
        builder.Property(i => i.Email);
        
        builder.Property(i => i.Photo);
        
        builder.Property(i => i.DateOfBirth);
    }
}