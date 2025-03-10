using Banking.Users.Application.Abstractions;
using Banking.Users.Domain;
using Banking.Users.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Banking.Users.Infrastructure.Seeding;

public class AccountManager(UsersWriteDbContext accountsWriteDbContext) : IAccountManager
{
    public async Task CreateAdminAccount(AdminAccount adminAccount)
    {
        accountsWriteDbContext.Add(adminAccount);
        
        await accountsWriteDbContext.SaveChangesAsync();
    }
    
    public async Task<bool> AdminAccountExists(CancellationToken cancellationToken = default)
    {
        return await accountsWriteDbContext.AdminAccounts.AnyAsync(cancellationToken);
    }
}