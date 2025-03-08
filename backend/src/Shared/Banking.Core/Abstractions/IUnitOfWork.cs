using System.Data;

namespace Banking.Core.Abstractions;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}