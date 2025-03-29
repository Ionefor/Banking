using Banking.ClientAccounts.Domain.Aggregate;
using Banking.ClientAccounts.Domain.Entities;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.BankAccounts.Application.Abstractions;

public interface IClientAccountService
{
    public Task<Result<ClientAccount, Error>> GetClientAccountIfExist(
        Guid clientAccountId,
        CancellationToken cancellationToken = default);
    
    public Task<Result<BankAccount, Error>> GetBankAccountIfExist(
        Guid bankAccountId,
        CancellationToken cancellationToken = default);
    
    public Task<Result<Card, Error>> GetCardIfExist(
        Guid cardId,
        CancellationToken cancellationToken = default);
}