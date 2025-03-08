using Banking.UserAccounts.Contracts;

namespace Banking.UserAccounts.Presentation;

public class UserAccountsContract : IUserAccountsContract
{
    public Task<HashSet<string>> GetUserPermissionsCodes(Guid userId)
    {
        throw new NotImplementedException();
    }
}