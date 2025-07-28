using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Web.Adapters.Out.TechnicalStuff.Json;

namespace Web.Api.DI;

public static class JsonOptionsRegistrations
{
    public static IServiceCollection AddJsonOptions(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options => { ConfigureJsonSerializerOptions(options.SerializerOptions); });
        return services;
    }

    private static void ConfigureJsonSerializerOptions(JsonSerializerOptions jsonSerializerOptions)
    {
        jsonSerializerOptions.PropertyNameCaseInsensitive = true;
        jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        jsonSerializerOptions.Converters.Add(new ValueObjectJsonConverterFactory());
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        jsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
    }
}