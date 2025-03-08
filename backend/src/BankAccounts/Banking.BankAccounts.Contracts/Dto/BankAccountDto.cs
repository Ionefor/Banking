using Banking.SharedKernel;

namespace Banking.BankAccounts.Contracts.Dto;

public class BankAccountDto
{
    public Guid Id { get; init; }
    
    public Guid UserAccountId { get; init; }
    
    public bool IsDeleted { get; init; }
    
    public AccountType AccountType { get; init; }
    
    public WalletDto[] Wallets { get; init; } = [];
    
    public CardDto[] Cards { get; init; } = [];
}