﻿using Banking.UserAccounts.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.UserAccounts.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddUserPresentation(this IServiceCollection services)
    {
        return services.AddScoped<IUserAccountsContract, UserAccountsContract>();
    }
}