using Microsoft.Extensions.DependencyInjection;

namespace Banking.BankAccounts.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddBankPresentation(this IServiceCollection services)
    {
       // return services.AddScoped<IVolunteersContract, VolunteersContract>();
       return services;
    }
}