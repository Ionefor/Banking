namespace Banking.Users.Application.Abstractions;

public interface IPermissionManager
{
    Task AddRangeIfExist(IEnumerable<string> permissions);
    Task<HashSet<string>> GetUserPermissions(Guid userId, CancellationToken cancellationToken = default);
}