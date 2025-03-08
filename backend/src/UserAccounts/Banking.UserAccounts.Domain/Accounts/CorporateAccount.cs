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
    
    public Name CompanyName  { get; init; } = null!;
    
    public Address Address  { get; init; } = null!;
    
    public TaxId TaxId  { get; init; } = null!;
    
    public Email ContactEmail { get; init; }
    
    public PhoneNumber ContactPhone { get; init; }
    
    //update: companyName, contactEmail, address, taxId, contactPhone
}