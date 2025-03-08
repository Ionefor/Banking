using Banking.BankAccounts.Domain.Aggregate;
using Banking.BankAccounts.Domain.Entities;
using Banking.BankAccounts.Domain.ValueObjects.Ids;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;

namespace Banking.BankAccounts.Application.Abstractions;

public interface IBankAccountRepository
{
    Task<Guid> Add(BankAccount bankAccount, CancellationToken cancellationToken = default);
    
    Guid Delete(BankAccount bankAccount, CancellationToken cancellationToken = default);
    
    Task<Result<BankAccount, Error>> GetById(AccountId accountId, CancellationToken cancellationToken = default);

    Task<Result<Wallet, Error>> GetWalletById(WalletId walletId, CancellationToken cancellationToken = default);
    
    Task<Result<Card, Error>> GetCardById(CardId cardId, CancellationToken cancellationToken = default);
}