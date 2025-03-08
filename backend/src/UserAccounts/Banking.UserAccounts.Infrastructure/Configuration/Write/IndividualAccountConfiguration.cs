using Banking.UserAccounts.Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.UserAccounts.Infrastructure.Configuration.Write;

public class IndividualAccountConfiguration : IEntityTypeConfiguration<IndividualAccount>
{
    public void Configure(EntityTypeBuilder<IndividualAccount> builder)
    {
        builder.ToTable("individual_accounts");
        
        builder.ComplexProperty(c => c.Address, ca =>
        {
            ca.Property(c => c.Country)
                .IsRequired()
                .HasColumnName("country");
            
            ca.Property(c => c.City)
                .IsRequired()
                .HasColumnName("city");
            
            ca.Property(c => c.Street)
                .IsRequired()
                .HasColumnName("street");
            
            ca.Property(c => c.HouseNumber)
                .IsRequired()
                .HasColumnName("house_number");
        });
        
        builder.ComplexProperty(c => c.Email, ce =>
        {
            ce.Property(e => e.Value)
                .IsRequired()
                .HasColumnName("email");
        });
        
        builder.ComplexProperty(c => c.DateOfBirth, cd =>
        {
            cd.Property(d => d.Value).
                IsRequired().HasColumnName("date_of_birth");
        });
        
        builder.ComplexProperty(c => c.PhoneNumber, cp =>
        {
            cp.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("phone_number");
        });
        
        builder.HasOne(v => v.User)
            .WithOne()
            .HasForeignKey<CorporateAccount>(v => v.UserId);
    }
}