﻿using Microsoft.Extensions.DependencyInjection;

namespace Banking.ClientAccounts.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddBankPresentation(this IServiceCollection services)
    {
       // return services.AddScoped<IVolunteersContract, VolunteersContract>();
       return services;
    }
}