using Banking.ClientAccounts.Domain.Aggregate;
using Banking.ClientAccounts.Domain.Entities;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.BankAccounts.Application.Abstractions;

public interface IClientAccountRepository
{
    Task Add(ClientAccount account, CancellationToken cancellationToken);

    Task Delete(ClientAccount account);
    
    Task<Result<ClientAccount, Error>> GetClientAccountById(
        ClientAccountId clientAccountId, CancellationToken cancellationToken = default);
    
    Task<Result<Account, Error>> GetAccountById(
        AccountId accountId, CancellationToken cancellationToken = default);
    
    Task<Result<Card, Error>> GetCardById(
        CardId cardId, CancellationToken cancellationToken = default);
}