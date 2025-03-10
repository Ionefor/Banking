using Banking.Users.Application.Abstractions;
using Banking.Users.Domain;
using Banking.Users.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Banking.Users.Infrastructure.Seeding;

public class PermissionManager(UsersWriteDbContext accountsWriteDbContext) : IPermissionManager
{
    public async Task AddRangeIfExist(IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var isPermissionExist = await accountsWriteDbContext.Permissions.
                AnyAsync(p => p.Code == permissionCode);

            if (!isPermissionExist)
            {
                await accountsWriteDbContext.Permissions.
                    AddAsync(new Permission { Code = permissionCode });
            }
        }
        
        await accountsWriteDbContext.SaveChangesAsync();
    }
    
    public async Task<HashSet<string>> GetUserPermissions(
        Guid userId, 
        CancellationToken cancellationToken = default)
    {
        var permissions = await accountsWriteDbContext.Users
            .Include(u => u.Roles)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .SelectMany(r=>r.RolePermission)
            .Select(rp=>rp.Permission.Code)
            .ToListAsync(cancellationToken);

        return permissions.ToHashSet();
    }
}