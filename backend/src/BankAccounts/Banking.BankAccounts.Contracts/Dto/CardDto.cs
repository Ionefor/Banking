namespace Banking.BankAccounts.Contracts.Dto;

public class CardDto
{
    public Guid Id { get; init; }
    
    public string PaymentDetails { get; init; }
    
    public Guid WalletId { get; init; }
    
    public string Ccv { get; init; }
    
    public DateTime ValidThru { get; init; }
    
    public bool IsDeleted { get; init; }
}