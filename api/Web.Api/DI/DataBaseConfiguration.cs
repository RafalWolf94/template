using Microsoft.EntityFrameworkCore;
using Web.Infrastructure.Common.Persistence;

namespace Web.Api.DI;

public static class DataBaseConfiguration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(x =>
        {
            x.EnableDetailedErrors();
            x.EnableSensitiveDataLogging();
            x.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions => { npgsqlOptions.EnableRetryOnFailure(); });
        });
        return services;
    }
}