using Banking.Users.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Users.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddUserPresentation(this IServiceCollection services)
    {
        return services.AddScoped<IUsersContract, UsersContract>();
    }
}