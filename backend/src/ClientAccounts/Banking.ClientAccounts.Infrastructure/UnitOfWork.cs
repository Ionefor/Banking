﻿using System.Data;
using Banking.ClientAccounts.Infrastructure.DbContexts;
using Banking.Core.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Banking.ClientAccounts.Infrastructure;

public class UnitOfWork(WriteDbContext dbContext) : IUnitOfWork
{
    public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        return transaction.GetDbTransaction();
    }
    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}