using Banking.Core.Abstractions;
using Banking.SharedKernel.Definitions;
using Banking.Users.Application.Abstractions;
using Banking.Users.Domain;
using Banking.Users.Infrastructure.Authorization;
using Banking.Users.Infrastructure.DbContexts;
using Banking.Users.Infrastructure.Options;
using Banking.Users.Infrastructure.Providers;
using Banking.Users.Infrastructure.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddUserInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContext()
            .AddCustomAuthorization()
            .AddJwtOptions(configuration)
            .AddAdminOptions(configuration)
            .AddJwtBearer(configuration).
            AddRolePermissionOptions(configuration).
            AddDatabase();

        return services;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(ModulesName.Users);

        return services;
    }
    private static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        services
            .AddIdentityCore<User>(options => options.User.RequireUniqueEmail = true)
            .AddRoles<Role>()
            .AddEntityFrameworkStores<UsersWriteDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<UsersWriteDbContext>();
        
        return services;
    }
    private static IServiceCollection AddRolePermissionOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<RolePermissionOptions>(configuration.
            GetSection(RolePermissionOptions.RolePermission));

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
    private static IServiceCollection AddAdminOptions(
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
        var jwtOptions = configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>()
                         ?? throw new ApplicationException("Missing JwtOptions configuration");
        
        services.
            AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
            AddJwtBearer(options =>
                options.TokenValidationParameters = TokenValidationParametersFactory
                    .CreateWithLifeTime(jwtOptions)); 
        
        services.AddAuthorization();
        services.AddScoped<ITokenProvider, JwtTokenProvider>();
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            
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
        
       // services.AddTransient<ITokenProvider, JwtTokenProvider>();
        
        return services;
    }
}