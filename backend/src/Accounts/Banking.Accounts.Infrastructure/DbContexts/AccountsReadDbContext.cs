using Banking.Accounts.Application.Abstractions;
using Banking.Accounts.Contracts.Dto.Models;
using Banking.SharedKernel.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Banking.Accounts.Infrastructure.DbContexts;

public class AccountsReadDbContext(IConfiguration configuration) : DbContext, IReadDbContext
{
    public IQueryable<IndividualAccountDto> IndividualAccounts => Set<IndividualAccountDto>();
    public IQueryable<CorporateAccountDto> CorporateAccounts => Set<CorporateAccountDto>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constants.Shared.Database));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AccountsReadDbContext).Assembly,
            type => type.FullName?.Contains(Constants.Shared.ConfigurationsRead) ?? false);
        
        modelBuilder.HasDefaultSchema(ModulesName.Accounts.ToString());
    }
    
    private ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}