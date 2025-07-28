using System.Reflection;

namespace Web.Presentation.Technical;

public static class TechnicalEndpoints
{
    public static void MapTechnicalEndpoints(this IEndpointRouteBuilder group)
    {
        var authGroup = group.MapGroup("technical")
            .WithTags("Technical")
            .WithOpenApi();

        authGroup.MapHealthChecks("/health")
            .WithOpenApi();

                  authGroup.MapGet("/version", () =>
            Results.Ok(new
            {
                Assembly.GetExecutingAssembly().GetName().Version,
            }))
            .WithName("GetVersion")
            .WithDescription("Get the current version of the API")
            .WithOpenApi();
    }
}