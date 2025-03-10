namespace Banking.Users.Domain;

public class Permission
{
    public Guid Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public List<RolePermission> RolePermissions { get; init; }
}