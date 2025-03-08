﻿using Banking.BankAccounts.Domain.Aggregate;
using Banking.SharedKernel.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Banking.BankAccounts.Infrastructure.DbContexts;

public class WriteDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<BankAccount> Accounts => Set<BankAccount>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constants.Shared.Database));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains(Constants.Shared.ConfigurationsWrite) ?? false);
        
        modelBuilder.HasDefaultSchema(ModulesName.BankAccounts.ToString());
    }
    private ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}