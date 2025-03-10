namespace Banking.Users.Infrastructure.Options;

public class RolePermissionOptions
{
    public const string RolePermission = nameof(RolePermission);
    public Dictionary<string, string[]> Permissions { get; init; } = [];
    public Dictionary<string, string[]> Roles { get; init; } = [];
}