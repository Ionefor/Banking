using Banking.ClientAccounts.Domain.Aggregate;
using Banking.ClientAccounts.Domain.Entities;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.BankAccounts.Application.Abstractions;

public interface IClientWriteRepository
{
    Task<Guid> Add(ClientAccount account, CancellationToken cancellationToken);

    Guid Delete(ClientAccount account);
    
    Task<Result<ClientAccount, Error>> GetClientAccountById(
        ClientAccountId clientAccountId, CancellationToken cancellationToken = default);
    
    Task<Result<BankAccount, Error>> GetBankAccountById(
        BankAccountId bankAccountId, CancellationToken cancellationToken = default);
    
    Task<Result<Card, Error>> GetCardById(
        CardId cardId, CancellationToken cancellationToken = default);
}