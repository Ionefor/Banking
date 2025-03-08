using Banking.UserAccounts.Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.UserAccounts.Infrastructure.Configuration.Write;

public class CorporateAccountConfiguration : IEntityTypeConfiguration<CorporateAccount>
{
    public void Configure(EntityTypeBuilder<CorporateAccount> builder)
    {
        builder.ToTable("corporate_accounts");
        
        builder.ComplexProperty(c => c.CompanyName, cn =>
        {
            cn.Property(e => e.Value)
                .IsRequired()
                .HasColumnName("company_name");
        });
        
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
        
        builder.ComplexProperty(c => c.TaxId, ct =>
        {
            ct.Property(t => t.Value)
                .IsRequired()
                .HasColumnName("tax_id");
        });
        
        builder.ComplexProperty(c => c.ContactEmail, ce =>
        {
            ce.Property(e => e.Value)
                .IsRequired()
                .HasColumnName("contact_email");
        });
        
        builder.ComplexProperty(c => c.ContactPhone, cp =>
        {
            cp.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("contact_phone");
        });
        
        builder.HasOne(v => v.User)
            .WithOne()
            .HasForeignKey<CorporateAccount>(v => v.UserId);
    }
}