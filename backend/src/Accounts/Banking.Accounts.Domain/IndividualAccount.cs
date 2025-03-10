using Banking.SharedKernel.ValueObjects;

namespace Banking.Accounts.Domain;

public class IndividualAccount
{
    private IndividualAccount() {}
    
    public IndividualAccount(
        Guid userId,
        FullName fullName,
        Address address,
        Email email,
        PhoneNumber phoneNumber,
        DateOfBirth dateOfBirth,
        FilePath photo)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        Photo = photo;
        FullName = fullName;
    }
    
    public Guid Id { get; init; }
    
    public Guid UserId { get; init; }
    
    public FullName FullName { get; private set; } = null!;
    
    public PhoneNumber PhoneNumber { get; private set; }
    
    public Address Address { get; private set; } = null!;
    
    public Email Email { get; private set; }
    
    public DateOfBirth DateOfBirth { get;  init; }
    
    public FilePath Photo { get;  private set; }

    public void UpdateName(FullName fullName)
    {
        FullName = fullName;
    }
    
    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }
    
    public void UpdateAddress(Address address)
    {
        Address = address;
    }
    
    public void UpdateEmail(Email email)
    {
        Email = email;
    }
    
    public void UpdatePhoto(FilePath photo)
    {
        Photo = photo;
    }
}