using System.Data;
using Banking.Core.Abstractions;
using Banking.Users.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Banking.Users.Infrastructure;

public class UnitOfWork(UsersWriteDbContext dbContext) : IUnitOfWork
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