using System.Data;
using Banking.Core.Abstractions;
using Banking.Users.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Banking.Users.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly UsersWriteDbContext _dbContext;

    public UnitOfWork(UsersWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IDbTransaction> BeginTransaction(
        CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Database.
            BeginTransactionAsync(cancellationToken);
        
        return transaction.GetDbTransaction();
    }
    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}