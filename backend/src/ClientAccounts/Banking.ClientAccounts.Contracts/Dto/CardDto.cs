namespace Banking.BankAccounts.Contracts.Dto;

public class CardDto
{
    public Guid Id { get; init; }
    
    public Guid ClientAccountId { get; init; }
    public string PaymentDetails { get; init; }
    
    public Guid AccountId { get; init; }
    
    public string Ccv { get; init; }
    
    public bool IsMain { get; init; }
    
    public DateTime ValidThru { get; init; }
    
    public bool IsDeleted { get; init; }
}