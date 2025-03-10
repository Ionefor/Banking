using Banking.Accounts.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Accounts.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsPresentation(this IServiceCollection services)
    {
        return services.AddScoped<IAccountsContract, AccountsContract>();
    }
}