using Web.Presentation.Account;
using Web.Presentation.Authorization;
using Web.Presentation.Technical;

namespace Web.Presentation;

public static class MapControllers
{
    public static void MapEndpoints(this IEndpointRouteBuilder endpoint)
    {
        var group = endpoint.MapGroup("api");
        group.MapAuthorizationEndpoints();
        group.MapAccountEndpoints();
        group.MapTechnicalEndpoints();
    }
}