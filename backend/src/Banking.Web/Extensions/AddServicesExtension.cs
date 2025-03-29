using Banking.Accounts.Application;
using Banking.Accounts.Infrastructure;
using Banking.Accounts.Presentation;
using Banking.BankAccounts.Application;
using Banking.ClientAccounts.Infrastructure;
using Banking.ClientAccounts.Presentation;
using Banking.Users.Application;
using Banking.Users.Infrastructure;
using Banking.Users.Presentation;
using FluentValidation;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace Banking.Web.Extensions;

public static class AddServicesExtension
{
     public static IServiceCollection AddServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBankModule().
            AddUserModule(configuration).
            AddAccountsModule(configuration).
            AddCustomSwaggerGen().
            AddValidation(configuration).
            AddLogging(configuration);

        return services;
    }
    
    private static IServiceCollection AddValidation(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        return services;
    }
    
    private static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Banking API",
                Version = "1"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Insert JWT token value",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement

            { 
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }
    
    private static IServiceCollection AddLogging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Debug()
            .WriteTo.Seq(configuration.GetConnectionString("Seq")
                         ?? throw new ArgumentNullException("Seq"))
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .CreateLogger();

        services.AddSerilog();
        services.AddHttpLogging(o => { o.CombineLogs = true; });

        return services;
    }
    
    private static IServiceCollection AddBankModule(
        this IServiceCollection services)
    {
        services.
            AddBankInfrastructure().
            AddBankApplication().
            AddBankPresentation();

        return services;
    }
    
    private static IServiceCollection AddUserModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.
            AddUserInfrastructure(configuration).
            AddUserApplication().
            AddUserPresentation();

        return services;
    }
    
    private static IServiceCollection AddAccountsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.
            AddAccountsInfrastructure(configuration).
            AddAccountsApplication().
            AddAccountsPresentation();

        return services;
    }
}