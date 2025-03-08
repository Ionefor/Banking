namespace Banking.UserAccounts.Contracts;

public interface IUserAccountsContract
{
    Task<HashSet<string>>  GetUserPermissionsCodes(Guid userId);
}