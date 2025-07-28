using System.Text.Json.Serialization;
using StackExchange.Redis;
using Web.Adapters.Out.Repositories;
using Web.Domain.Authorization;
using Web.Domain.TechnicalStuff.FileStorage;
using Web.Domain.TechnicalStuff.Redis;
using Web.Infrastructure.Services.Auth;

namespace Web.Api.DI;

public static class ApiConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddControllers();
        services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));
        services.Configure<FileStorageSettings>(configuration.GetSection("FileStorage"));
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IPasswordService, PasswordService>();
        services.AddAuthentication();

        var redisSettings = configuration.GetSection("Redis").Get<RedisSettings>();
        if (redisSettings is not null)
        {
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaximumReceiveMessageSize = 102400;
            }).AddStackExchangeRedis(options =>
            {
                options.ConnectionFactory = async _ =>
                {
                    var connection = await ConnectionMultiplexer.ConnectAsync(new ConfigurationOptions
                    {
                        EndPoints = { $"{redisSettings.Host}:{redisSettings.Port}" },
                        Password = redisSettings.Password,
                        AbortOnConnectFail = false
                    });
                    return connection;
                };

                options.Configuration.ChannelPrefix = RedisChannel.Literal(redisSettings.ChannelPrefix);
            });
        }
        else
        {
            services.AddSignalR();
        }

        services.AddControllers().AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });
        services.AddHealthChecks();

        return services;
    }
}