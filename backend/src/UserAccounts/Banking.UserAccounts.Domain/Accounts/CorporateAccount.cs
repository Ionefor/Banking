using Banking.UserAccounts.Domain.ValueObjects;

namespace Banking.UserAccounts.Domain.Accounts;

public class CorporateAccount
{
    public static string Corporate = nameof(Corporate);
    
    private CorporateAccount() {}
    
    public CorporateAccount(
        User user,
        Name companyName,
        Address address,
        TaxId taxId,
        Email contactEmail,
        PhoneNumber contactPhone)
    {
        Id = Guid.NewGuid();
        User = user;
        CompanyName = companyName;
        Address = address;
        TaxId = taxId;
        ContactEmail = contactEmail;
        ContactPhone = contactPhone;
    }
    
    public Guid Id { get; init; }
    
    public User User { get; init; }
    
    public Guid UserId { get; init; }
    
    public Name CompanyName  { get; private set; } = null!;
    
    public Address Address  { get; private set; } = null!;
    
    public TaxId TaxId  { get; private set; } = null!;
    
    public Email ContactEmail { get; private set; }
    
    public PhoneNumber ContactPhone { get; private set; }
    
    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        ContactPhone = phoneNumber;
    }
    
    public void UpdateAddress(Address address)
    {
        Address = address;
    }
    
    public void UpdateEmail(Email email)
    {
        ContactEmail = email;
    }
    
    public void UpdateTaxId(TaxId taxId)
    {
        TaxId = taxId;
    }
    
    public void UpdateCompanyName(Name companyName)
    {
        CompanyName = companyName;
    }
}