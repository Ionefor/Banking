using Banking.BankAccounts.Application.Abstractions;
using Banking.ClientAccounts.Infrastructure.DbContexts;
using Banking.ClientAccounts.Infrastructure.Repositories;
using Banking.Core.Abstractions;
using Banking.SharedKernel.Definitions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.ClientAccounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddBankInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContexts().
            AddDatabase().
            AddRepositories();

        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
      services.AddScoped<IClientAccountRepository, ClientAccountRepository>();

        return services;
    }
    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }
    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(ModulesName.ClientAccounts);

        return services;
    }
}