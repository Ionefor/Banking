using Banking.Accounts.Domain;
using Banking.SharedKernel.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.Accounts.Infrastructure.Configurations.Write;

public class IndividualAccountConfiguration : IEntityTypeConfiguration<IndividualAccount>
{
    public void Configure(EntityTypeBuilder<IndividualAccount> builder)
    {
        builder.ToTable("individual_accounts");
        
        builder.Property(c => c.UserId);
        
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
        
        builder.ComplexProperty(v => v.FullName, fb =>
        {
            fb.Property(f => f.FirstName)
                .IsRequired()
                .HasMaxLength(Constants.Shared.MaxLowTextLength)
                .HasColumnName("firstName");

            fb.Property(f => f.MiddleName)
                .IsRequired()
                .HasMaxLength(Constants.Shared.MaxLowTextLength)
                .HasColumnName("middleName");

            fb.Property(f => f.LastName)
                .IsRequired(false)
                .HasMaxLength(Constants.Shared.MaxLowTextLength)
                .HasColumnName("lastName");
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
        
        builder.ComplexProperty(c => c.Photo, cp =>
        {
            cp.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("photo");
        });
    }
}