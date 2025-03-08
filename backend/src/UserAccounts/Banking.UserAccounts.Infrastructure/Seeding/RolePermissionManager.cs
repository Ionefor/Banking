using Banking.UserAccounts.Domain;
using Banking.UserAccounts.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Banking.UserAccounts.Infrastructure.Seeding;

public class RolePermissionManager(UserAccountsWriteDbContext accountsWriteDbContext)
{
    public async Task AddRangeIfExist(Guid roleId, IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var permission = await accountsWriteDbContext.Permissions.
                FirstOrDefaultAsync(p => p.Code == permissionCode);
            
            var rolePermissionExist = await accountsWriteDbContext.RolePermission.
                AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission!.Id);

            if (!rolePermissionExist)
            {
                await accountsWriteDbContext.RolePermission.AddAsync(new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permission!.Id
                });
            }
        }
        
        await accountsWriteDbContext.SaveChangesAsync();
    }
}