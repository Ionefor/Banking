using Banking.BankAccounts.Application.Abstractions;
using Banking.ClientAccounts.Domain.Aggregate;
using Banking.ClientAccounts.Domain.Entities;
using Banking.ClientAccounts.Domain.ValueObjects.Ids;
using Banking.ClientAccounts.Infrastructure.DbContexts;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Banking.ClientAccounts.Infrastructure.Repositories;

public class ClientWriteRepository(WriteDbContext dbContext) :
    IClientWriteRepository
{
    public async Task<Guid> Add(ClientAccount clientAccount, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(clientAccount, cancellationToken);

        return clientAccount.Id;
    }

    public Guid Delete(ClientAccount clientAccount)
    {
        dbContext.Remove(clientAccount);
        
        return clientAccount.Id;
    }

    public async Task<Result<ClientAccount, Error>> GetClientAccountById(
        ClientAccountId clientAccountId, CancellationToken cancellationToken = default)
    {
        return (await dbContext.ClientAccounts.
            Include(c => c.Accounts).
            Include(c => c.Cards).
            FirstOrDefaultAsync(c => c.Id == clientAccountId, cancellationToken))!;
    }

    public async Task<Result<BankAccount, Error>> GetBankAccountById(
        BankAccountId bankAccountId,
        CancellationToken cancellationToken = default)
    {
        var clientAccounts = dbContext.
            ClientAccounts.Include(c => c.Accounts);
        
        var clientAccount = await clientAccounts.FirstOrDefaultAsync(
            c => c.Accounts.Any(a => a.Id == bankAccountId), cancellationToken);

        if (clientAccount is null)
        {
            return Errors.General.
                NotFound(new ErrorParameters.NotFound(nameof(ClientAccounts),
                    nameof(BankAccountId), bankAccountId));
        }
        
        var account = clientAccount.Accounts.
            FirstOrDefault(a => a.Id == bankAccountId)!;
        
        return account;
    }

    public async Task<Result<Card, Error>> GetCardById(
        CardId cardId, CancellationToken cancellationToken = default)
    {
        var clientAccounts = dbContext.
            ClientAccounts.Include(c => c.Cards);
        
        var clientAccount = await clientAccounts.FirstOrDefaultAsync(
            c => c.Cards.Any(a => a.Id == cardId), cancellationToken);

        if (clientAccount is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.NotFound(nameof(ClientAccounts), nameof(CardId), cardId));
        }
        
        var card = clientAccount.
            Cards.FirstOrDefault(a => a.Id == cardId)!;
        
        return card;
    }
}