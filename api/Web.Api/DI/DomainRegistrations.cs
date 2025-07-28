using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Web.Adapters.Out;
using Web.Adapters.Out.AppUsers;
using Web.Adapters.Out.AppUsers.Get;
using Web.Adapters.Out.Repositories;
using Web.Domain.Repositories;
using Web.Domain.TechnicalStuff.Redis;
using Web.Infrastructure.Services.Onboarding;
using Web.Infrastructure.Services.Outbox;
using Web.UseCases;
using Web.UseCases.TechnicalStuff.Cqrs;
using Web.UseCases.TechnicalStuff.Transactions;

namespace Web.Api.DI;

public static class DomainRegistrations
{
    public static IServiceCollection AddDomainModel(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHandlers()
            .AddScoped<ITransactionContext, TransactionContext>()
            .AddScoped<IAppUserRepository, AppUserRepository>()
            .Configure<EmailSettings>(configuration.GetSection("EmailSettings"))
            .Configure<OnboardingSettings>(configuration.GetSection("OnboardingSettings"));
        services.AddQueries();
        services.AddRedis(configuration);
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services
            .Scan(selector => selector.FromAssemblies(
                    AdaptersOutLayerInfo.Assembly,
                    UseCasesLayerInfo.Assembly)
                .AddClasses(filter => filter.AssignableToAny(
                    typeof(ICommandHandler<>),
                    typeof(ICommandHandler<,>),
                    typeof(IQueryHandler<,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

        services.Decorate(typeof(ICommandHandler<>), typeof(TransactionCommandHandlerDecorator<>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(TransactionCommandHandlerDecorator<,>));
        return services;
    }

    private static void AddQueries(this IServiceCollection services)
    {
        services
            .AddScoped<IQueryHandler<GetAccount.Query, GetAccount.Data>, AccountQueries>();
    }

    private static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisSettings>(configuration.GetSection("Redis"));

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;

            var configOptions = new ConfigurationOptions
            {
                EndPoints = { $"{redisSettings.Host}:{redisSettings.Port}" },
                Password = redisSettings.Password,
                AbortOnConnectFail = false,
            };

            return ConnectionMultiplexer.Connect(configOptions);
        });
    }
}