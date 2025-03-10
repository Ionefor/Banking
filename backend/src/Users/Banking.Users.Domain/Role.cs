using Microsoft.AspNetCore.Identity;

namespace Banking.Users.Domain;

public class Role : IdentityRole<Guid>
{
    public List<RolePermission> RolePermission { get; init; }
    
    public List<User> Users { get; init; } = [];
}