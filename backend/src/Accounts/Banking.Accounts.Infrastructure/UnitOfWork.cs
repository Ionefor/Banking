using System.Data;
using Banking.Accounts.Infrastructure.DbContexts;
using Banking.Core.Abstractions;
using Banking.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage;

namespace Banking.Accounts.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AccountsWriteDbContext _dbContext;

    public UnitOfWork(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IDbTransaction> BeginTransaction(
        CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        return transaction.GetDbTransaction();
    }
    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}