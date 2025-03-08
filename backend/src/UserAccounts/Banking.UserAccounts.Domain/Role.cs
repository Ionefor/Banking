using Microsoft.AspNetCore.Identity;

namespace Banking.UserAccounts.Domain;

public class Role : IdentityRole<Guid>
{
    public List<RolePermission> RolePermission { get; init; }
    
    public List<User> Users { get; init; } = [];
}