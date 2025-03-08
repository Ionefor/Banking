using Banking.SharedKernel;

namespace Banking.BankAccounts.Contracts.Dto;

public class WalletDto
{
    public Guid Id { get; init; }
    
    public string PaymentDetails { get; init; }
    
    public WalletType Type { get; init; }
    
    public Currencies Сurrency  { get; init; }
    
    public double Balance { get; init; }
    
    public bool IsDeleted { get; init; }
}