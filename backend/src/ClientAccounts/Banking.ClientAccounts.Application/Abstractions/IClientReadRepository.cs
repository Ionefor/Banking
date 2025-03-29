namespace Banking.BankAccounts.Application.Abstractions;

public interface IClientReadRepository
{
    public Task<bool> ClientAccountExist(
        Guid clientAccountId, CancellationToken cancellationToken = default);
    
    public Task<bool> BankAccountExist(
        Guid bankAccountId, CancellationToken cancellationToken = default);
    
    public Task<bool> CardExist(
        Guid cardId, CancellationToken cancellationToken = default);
    
    public Task<bool> CardExist(
        string paymentDetails, CancellationToken cancellationToken = default);
}