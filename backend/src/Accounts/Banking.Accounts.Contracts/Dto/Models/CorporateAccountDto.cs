using Banking.Accounts.Contracts.Dto.Commands;

namespace Banking.Accounts.Contracts.Dto.Models;

public class CorporateAccountDto
{
    public Guid Id { get; init; }
    
    public Guid UserId { get; init; }
    
    public string CompanyName  { get; init; } = null!;
    
    public AddressDto Address  { get; init; } = null!;
    
    public string TaxId  { get; init; } = null!;
    
    public string ContactEmail { get; init; } = null!;
    
    public string ContactPhone { get; init; } = null!;
}