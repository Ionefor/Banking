using Banking.Core.Abstractions;
using Banking.SharedKernel.Definitions;
using Banking.UserAccounts.Application.Abstractions;
using Banking.UserAccounts.Domain;
using Banking.UserAccounts.Infrastructure.Authorization;
using Banking.UserAccounts.Infrastructure.DbContexts;
using Banking.UserAccounts.Infrastructure.Options;
using Banking.UserAccounts.Infrastructure.Providers;
using Banking.UserAccounts.Infrastructure.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.UserAccounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddUserInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContext()
            .AddCustomAuthorization()
            .AddJwtOptions(configuration)
            .AddAdminOptins(configuration)
            .AddJwtBearer(configuration).
            AddDatabase().
            AddRolePermissionOptions(configuration);

        return services;
    }
    private static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        services
            .AddIdentityCore<User>(options => options.User.RequireUniqueEmail = true)
            .AddRoles<Role>()
            .AddEntityFrameworkStores<UserAccountsWriteDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<UserAccountsWriteDbContext>();
        
        return services;
    }
    private static IServiceCollection AddRolePermissionOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<RolePermissionOptions>(
            configuration.GetSection(RolePermissionOptions.RolePermission));

        services.AddOptions<RolePermissionOptions>();
        
        return services;
    }
    
    private static IServiceCollection AddJwtOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.Jwt));

        services.AddOptions<JwtOptions>();
        
        return services;
    }
    private static IServiceCollection AddAdminOptins(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AdminOptions>(
            configuration.GetSection(AdminOptions.Admin));

        services.AddOptions<AdminOptions>();
        
        return services;
    }
    private static IServiceCollection AddJwtBearer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.
            AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
            AddJwtBearer(options =>
            {
                var jwtOptions = configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>()
                                 ?? throw new ApplicationException("Missing JWT configuration");
                
                options.TokenValidationParameters =
                    TokenValidationParametersFactory.CreateWithLifeTime(jwtOptions);
            });
        
        services.AddAuthorization();
        
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            
        return services;
    }
    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(ModulesName.UserAccounts);
        
        return services;
    }
    private static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {                
        services.AddScoped<IPermissionManager, PermissionManager>();
        services.AddScoped<PermissionManager>();
        services.AddScoped<RolePermissionManager>();
        services.AddScoped<IAccountManager, AccountManager>();
        services.AddScoped<IRefreshSessionManager, RefreshSessionManager>();
        
        services.AddSingleton<AccountSeeder>();
        services.AddScoped<AccountsSeederService>();
        
        services.AddTransient<ITokenProvider, JwtTokenProvider>();
        
        return services;
    }
}