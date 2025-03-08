using System.Data;
using Banking.Core.Abstractions;
using Banking.UserAccounts.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Banking.UserAccounts.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly UserAccountsWriteDbContext _dbContext;
    
    public UnitOfWork(UserAccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
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