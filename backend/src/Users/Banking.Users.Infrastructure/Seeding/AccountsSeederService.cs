using System.Text.Json;
using Banking.SharedKernel.Definitions;
using Banking.Users.Application.Abstractions;
using Banking.Users.Domain;
using Banking.Users.Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Banking.Users.Infrastructure.Seeding;

public class AccountsSeederService(
    RoleManager<Role> roleManager,
    PermissionManager permissionManager,
    RolePermissionManager rolePermissionManager,
    IAccountManager accountManager,
    UserManager<User> userManager,
    ILogger<AccountSeeder> logger,
    IOptions<AdminOptions> adminOptions)
{
    private readonly AdminOptions _adminOptions = adminOptions.Value;
    
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        var json = await File.ReadAllTextAsync(Constants.Accounts.AccountsPath, cancellationToken);

        var seedData = JsonSerializer.Deserialize<RolePermissionOptions>(json)
                       ?? throw new ApplicationException("Could not deserialize role permission config");

        await SeedPermissions(seedData);

        await SeedRoles(seedData);

        await SeedRolePermissions(seedData);
        
        await SeedAdmin();
    }

    private async Task SeedAdmin()
    {
        var adminRole = await roleManager.FindByNameAsync(AdminAccount.Admin)
                        ?? throw new ApplicationException("Seeding error: unable to find admin role");

        if (await accountManager.AdminAccountExists())
        {
            logger.LogInformation("Admin account already exists in database, aborting admin seeding");
            return;
        }

        var adminUser = User.CreateAdmin(_adminOptions.UserName, _adminOptions.Email, adminRole);
        var creationResult = await userManager.CreateAsync(adminUser.Value, _adminOptions.Password);
        if (creationResult.Succeeded == false)
            throw new ApplicationException(creationResult.Errors.First().Description);
        

        var adminAccount = AdminAccount.Create(adminUser.Value);
        await accountManager.CreateAdminAccount(adminAccount);

        logger.LogInformation("Successfully seeded admin account to database");
    }
    private async Task SeedPermissions(RolePermissionOptions seedData)
    {
        var permissionsToAdd = seedData.Permissions.
            SelectMany(permissions => permissions.Value);

        await permissionManager.AddRangeIfExist(permissionsToAdd);
        
        logger.LogInformation("Permissions added to seed data");
    }
    private async Task SeedRoles(RolePermissionOptions seedData)
    {
        foreach (var role in seedData.Roles.Keys)
        {
            var existingRole = await roleManager.FindByNameAsync(role);

            if (existingRole is null)
            {
                await roleManager.CreateAsync(new Role { Name = role });
            }
        }
        
        logger.LogInformation("Role added to database.");
    }
    private async Task SeedRolePermissions(RolePermissionOptions seedData)
    {
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            
            var rolePermissions = seedData.Roles[roleName];
            
            await rolePermissionManager.AddRangeIfExist(role!.Id, rolePermissions);
        }
        
        logger.LogInformation("Role permissions added to database.");
    }
}