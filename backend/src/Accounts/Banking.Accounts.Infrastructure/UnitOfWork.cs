using System.Data;
using Banking.Accounts.Infrastructure.DbContexts;
using Banking.Core.Abstractions;
using Banking.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage;

namespace Banking.Accounts.Infrastructure;

public class UnitOfWork(AccountsWriteDbContext dbContext) : IUnitOfWork
{
    public async Task<IDbTransaction> BeginTransaction(
        CancellationToken cancellationToken = default)
    {
        var transaction = await dbContext.Database.
            BeginTransactionAsync(cancellationToken);
        
        return transaction.GetDbTransaction();
    }
    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}