namespace Banking.UserAccounts.Domain;

public class RolePermission
{
    public Guid RoleId { get; init; }
    public Role Role { get; init; }
    public Guid PermissionId { get; init; }
    public Permission Permission { get; init; }
}