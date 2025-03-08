using Banking.SharedKernel.Definitions;
using Banking.UserAccounts.Domain;
using Banking.UserAccounts.Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Banking.UserAccounts.Infrastructure.DbContexts;

public class UserAccountsWriteDbContext(IConfiguration configuration)
    : IdentityDbContext<User, Role, Guid>
{
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermission => Set<RolePermission>();
    
    public DbSet<AdminAccount> AdminAccounts => Set<AdminAccount>();
    public DbSet<CorporateAccount> CorporateAccount => Set<CorporateAccount>();
    public DbSet<IndividualAccount> IndividualAccount => Set<IndividualAccount>();
    
    public DbSet<RefreshSession> RefreshSessions => Set<RefreshSession>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constants.Shared.Database));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Role>()
            .ToTable("roles");
        
        modelBuilder.Entity<IdentityUserClaim<Guid>>()
            .ToTable("user_claims");
        
        modelBuilder.Entity<IdentityUserToken<Guid>>()
            .ToTable("user_tokens");
        
        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .ToTable("user_logins");
        
        modelBuilder.Entity<IdentityRoleClaim<Guid>>()
            .ToTable("role_claims");
        
        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .ToTable("user_roles");
        
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(UserAccountsWriteDbContext).Assembly,
            type => type.FullName?.Contains(Constants.Shared.ConfigurationsWrite) ?? false);
        
        modelBuilder.HasDefaultSchema("user_accounts");
    }
    private ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}