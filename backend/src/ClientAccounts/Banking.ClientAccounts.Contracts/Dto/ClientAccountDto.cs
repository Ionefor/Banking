using Banking.SharedKernel;

namespace Banking.BankAccounts.Contracts.Dto;

public class ClientAccountDto
{
    public Guid Id { get; init; }
    
    public Guid AccountId { get; init; }
    
    public bool IsDeleted { get; init; }
    
    public AccountType UserAccountType { get; init; }
    
    public AccountDto[] Accounts { get; init; } = [];
    
    public CardDto[] Cards { get; init; } = [];
}