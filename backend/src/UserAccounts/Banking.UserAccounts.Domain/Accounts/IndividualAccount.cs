using Banking.UserAccounts.Domain.ValueObjects;

namespace Banking.UserAccounts.Domain.Accounts;

public class IndividualAccount 
{
    public static string Individual = nameof(Individual);
    
    private IndividualAccount() {}
    
    public IndividualAccount(
        User user,
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
    }
    
    public Guid Id { get; init; }
    public User User { get; init; }
    
    public Guid UserId { get; init; }
    
    public PhoneNumber PhoneNumber { get; init; }
    
    public Address Address { get; init; } = null!;
    
    public Email Email { get; init; }
    
    public DateOfBirth DateOfBirth { get; init; }
    
    public FilePath Photo { get; init; }
    
    //update: number, photo, email, address
}