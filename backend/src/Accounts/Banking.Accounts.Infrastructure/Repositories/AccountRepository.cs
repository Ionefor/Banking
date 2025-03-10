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

    public async Task<Result<IndividualAccount, Error>> GetIndividualByUserId(
        Guid userId, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.IndividualAccounts.
            FirstOrDefaultAsync(i => i.UserId == userId, cancellationToken);

        if (result is null)
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound
                (nameof(IndividualAccount), nameof(userId), userId));
        }
        
        return result;
    }

    public async Task<Result<CorporateAccount, Error>> GetCorporateByUserId(
        Guid userId, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.CorporateAccounts.
            FirstOrDefaultAsync(i => i.UserId == userId, cancellationToken);

        if (result is null)
        {
            return Errors.General.NotFound(new ErrorParameters.NotFound
                (nameof(CorporateAccount), nameof(userId), userId));
        }
        
        return result;
    }
}