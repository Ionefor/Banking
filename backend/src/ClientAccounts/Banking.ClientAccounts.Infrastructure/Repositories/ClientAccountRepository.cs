using Banking.BankAccounts.Application.Abstractions;
using Banking.ClientAccounts.Domain.Aggregate;
using Banking.ClientAccounts.Domain.Entities;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.ClientAccounts.Infrastructure.DbContexts;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Banking.ClientAccounts.Infrastructure.Repositories;

public class ClientAccountRepository :
    IClientAccountRepository
{
    private readonly WriteDbContext _dbContext;
    
    public ClientAccountRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Add(ClientAccount account, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(account, cancellationToken);
    }

    public Task Delete(ClientAccount account)
    {
        _dbContext.Remove(account);
        
        return Task.CompletedTask;
    }

    public async Task<Result<ClientAccount, Error>> GetClientAccountById(
        ClientAccountId clientAccountId, CancellationToken cancellationToken = default)
    {
        var clientAccount = await _dbContext.ClientAccounts.FirstOrDefaultAsync(
            c => c.Id == clientAccountId, cancellationToken);

        if (clientAccount is null)
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound
                (nameof(ClientAccounts), nameof(clientAccountId), clientAccountId));
        }

        return clientAccount;
    }

    public async Task<Result<Account, Error>> GetAccountById(
        AccountId accountId,
        CancellationToken cancellationToken = default)
    {
        var clientAccounts = _dbContext.
            ClientAccounts.Include(c => c.Accounts);
        
        var clientAccount = await clientAccounts.FirstOrDefaultAsync(
            c => c.Accounts.Any(a => a.Id == accountId), cancellationToken);

        if (clientAccount is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(ClientAccounts), nameof(AccountId), accountId));
        }
        
        var account = clientAccount.Accounts.
            FirstOrDefault(a => a.Id == accountId);
        
        if (account is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(Account), nameof(AccountId), accountId));
        }
        
        return account;
    }

    public async Task<Result<Card, Error>> GetCardById(
        CardId cardId, CancellationToken cancellationToken = default)
    {
        var clientAccounts = _dbContext.
            ClientAccounts.Include(c => c.Cards);
        
        var clientAccount = await clientAccounts.FirstOrDefaultAsync(
            c => c.Cards.Any(a => a.Id == cardId), cancellationToken);

        if (clientAccount is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(ClientAccounts), nameof(CardId), cardId));
        }
        
        var card = clientAccount.
            Cards.FirstOrDefault(a => a.Id == cardId);
        
        if (card is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(card), nameof(CardId), cardId));
        }
        
        return card;
    }
}