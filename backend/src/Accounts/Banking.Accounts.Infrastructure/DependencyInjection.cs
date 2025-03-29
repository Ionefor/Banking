using Banking.Accounts.Application;
using Banking.Accounts.Application.Abstractions;
using Banking.Accounts.Application.PhotoProvider;
using Banking.Accounts.Infrastructure.DbContexts;
using Banking.Accounts.Infrastructure.MessageQueues;
using Banking.Accounts.Infrastructure.Options;
using Banking.Accounts.Infrastructure.Providers;
using Banking.Accounts.Infrastructure.Repositories;
using Banking.Core.Abstractions;
using Banking.Core.Messaging;
using Minio;
using Banking.SharedKernel.Definitions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Accounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContexts().
            AddRepositories().
            AddFilesService(configuration).
            AddMessageQueues().
            AddDatabase();

        return services;
    }
    private static IServiceCollection AddFilesService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MinioOptions>(
            configuration.GetSection(MinioOptions.Minio));
        
        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.Minio).Get<MinioOptions>()
                               ?? throw new ApplicationException();
    
            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.WithSsl);
        });
    
        services.AddScoped<IPhotoProvider, MinioProvider>();
    
        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<AccountsWriteDbContext>();
        services.AddScoped<IReadDbContext, AccountsReadDbContext>();

        return services;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(ModulesName.Accounts);

        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        
        return services;
    }
    
    private static IServiceCollection AddMessageQueues(this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueue<PhotoMetaData>,
            InMemoryMessageQueue<PhotoMetaData>>();
    
        return services;
    }
}