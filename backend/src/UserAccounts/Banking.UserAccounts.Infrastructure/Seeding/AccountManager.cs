using Banking.UserAccounts.Application.Abstractions;
using Banking.UserAccounts.Domain.Accounts;
using Banking.UserAccounts.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Banking.UserAccounts.Infrastructure.Seeding;

public class AccountManager(UserAccountsWriteDbContext accountsWriteDbContext) : IAccountManager
{
    public async Task CreateAdminAccount(AdminAccount adminAccount)
    {
        accountsWriteDbContext.Add(adminAccount);
        
        await accountsWriteDbContext.SaveChangesAsync();
    }

    public async Task CreateIndividualAccount(IndividualAccount individualAccount)
    {
        accountsWriteDbContext.Add(individualAccount);
        
        await accountsWriteDbContext.SaveChangesAsync();
    }

    public async Task CreateCorporateAccount(CorporateAccount corporateAccount)
    {
        accountsWriteDbContext.Add(corporateAccount);
        
        await accountsWriteDbContext.SaveChangesAsync();
    }

    public async Task<(IndividualAccount?, CorporateAccount?)> GetAccountByUserId(Guid userId)
    {
        var individualAccount = await accountsWriteDbContext.IndividualAccount.
            FirstOrDefaultAsync(x => x.UserId == userId);
      
        if(individualAccount is not null)
            return (individualAccount, null);
        
        var corporateAccount = await accountsWriteDbContext.CorporateAccount
            .FirstOrDefaultAsync(a => a.UserId == userId);
      
        return (individualAccount, corporateAccount);
    }

    public Task DeleteIndividualAccount(IndividualAccount individualAccount)
    {
        accountsWriteDbContext.IndividualAccount.Remove(individualAccount);
        return Task.CompletedTask;
    }

    public Task DeleteCorporateAccount(CorporateAccount corporateAccount)
    {
        accountsWriteDbContext.CorporateAccount.Remove(corporateAccount);
        return Task.CompletedTask;
    }


    public async Task<bool> AdminAccountExists(CancellationToken cancellationToken = default)
    {
        return await accountsWriteDbContext.AdminAccounts.AnyAsync(cancellationToken);
    }
}