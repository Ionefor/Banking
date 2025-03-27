using Banking.Accounts.Application;
using Banking.Accounts.Application.Abstractions;
using Banking.Accounts.Domain;
using Banking.Accounts.Infrastructure.DbContexts;
using Banking.SharedKernel.Models.Errors;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Banking.Accounts.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AccountsWriteDbContext _dbContext;
    
    public AccountRepository(AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Add<TAccount>(TAccount account, CancellationToken cancellationToken)
        where TAccount : class
    {
        await _dbContext.Set<TAccount>().AddAsync(account, cancellationToken);
    }

    public Task Delete<TAccount>(TAccount account) where TAccount : class
    {
        _dbContext.Set<TAccount>().Remove(account);
        
        return Task.CompletedTask;
    }

    public async Task<Result<IndividualAccount, Error>> GetIndividualById(
        Guid accountId, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.IndividualAccounts.
            FirstOrDefaultAsync(i => i.Id == accountId, cancellationToken);

        if (result is null)
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound
                (nameof(IndividualAccount), nameof(accountId), accountId));
        }
        
        return result;
    }

    public async Task<Result<CorporateAccount, Error>> GetCorporateById(
        Guid accountId, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.CorporateAccounts.
            FirstOrDefaultAsync(c => c.Id == accountId, cancellationToken);

        if (result is null)
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound
                (nameof(CorporateAccount), nameof(accountId), accountId));
        }
        
        return result;
    }
}