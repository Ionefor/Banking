using Banking.SharedKernel.Models.Abstractions;
using Banking.UserAccounts.Domain.ValueObjects;

namespace Banking.UserAccounts.Domain.Accounts;

public class IndividualAccount
{
    public static string Individual = nameof(Individual);
    
    private IndividualAccount() {}
    
    public IndividualAccount(
        User user,
        FullName fullName,
        Address address,
        Email email,
        PhoneNumber phoneNumber,
        DateOfBirth dateOfBirth,
        FilePath photo)
    {
        Id = Guid.NewGuid();
        User = user;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        Photo = photo;
        FullName = fullName;
    }
    
    public Guid Id { get; init; }
    public User User { get; init; }
    
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