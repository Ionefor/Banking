using Banking.Accounts.Contracts.Dto.Commands;

namespace Banking.Accounts.Contracts.Dto.Models;

public class IndividualAccountDto
{
    public Guid Id { get; init; }
    
    public Guid UserId { get; init; }
    
    public FullNameDto FullName { get; init; } = null!;
    
    public string PhoneNumber { get; init; } = null!;
    
    public AddressDto Address { get; init; } = null!;
    
    public string Email { get; init; } = null!;
    
    public DateOnly DateOfBirth { get;  init; }
    
    public string Photo { get;  init; } = null!;
}