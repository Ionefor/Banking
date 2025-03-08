using Banking.Core.Abstractions;
using Banking.SharedKernel.Definitions;
using Banking.UserAccounts.Infrastructure.DbContexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.UserAccounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContexts().AddFilesService(configuration).AddMessageQueues().AddDatabase();

        return services;
    }
    private static IServiceCollection AddFilesService(this IServiceCollection services,
        IConfiguration configuration)
    {
        // services.Configure<MinioOptions>(
        //     configuration.GetSection(MinioOptions.Minio));
        //
        // ServiceCollectionExtensions.AddMinio(services, options =>
        // {
        //     var minioOptions = configuration.GetSection(MinioOptions.Minio).Get<MinioOptions>()
        //                        ?? throw new ApplicationException();
        //
        //     options.WithEndpoint(minioOptions.Endpoint);
        //     options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
        //     options.WithSSL(minioOptions.WithSsl);
        // });
        //
        // services.AddScoped<IPhotoProvider, MinioProvider>();

        return services;
    }
    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<UserAccountsWriteDbContext>();
        //services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }
    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(ModulesName.UserAccounts);

        return services;
    }
    private static IServiceCollection AddMessageQueues(this IServiceCollection services)
    {
        // services.AddSingleton<IMessageQueue<IEnumerable<PhotoMetaData>>,
        //     InMemoryMessageQueue<IEnumerable<PhotoMetaData>>>();

        return services;
    }
}