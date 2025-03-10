using Banking.Users.Domain;

namespace Banking.Users.Application.Abstractions;

public interface IAccountManager
{
    public Task CreateAdminAccount(AdminAccount adminAccount);
    
    public Task<bool> AdminAccountExists(CancellationToken cancellationToken = default);
}