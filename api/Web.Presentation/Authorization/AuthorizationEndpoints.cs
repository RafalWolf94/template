using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Web.UseCases.Authorization.Authorize;
using Web.UseCases.TechnicalStuff.Cqrs;

namespace Web.Presentation.Authorization;

public static class AuthorizationEndpoints
{
    public static void MapAuthorizationEndpoints(this IEndpointRouteBuilder group)
    {
        var authGroup = group.MapGroup("auth")
            .WithTags("Authorization")
            .WithOpenApi();

        authGroup.MapPost("/", HandleAuth)
            .Produces<Ok<Authorize.Data>>()
            .Accepts<Authorize.Command>("application/json")
            .WithOpenApi();
        
        authGroup.MapPost("/logout",TypedResults.Ok)
            .Produces<Ok>()
            .WithOpenApi();
    }

    private static async Task<Ok<Authorize.Data>> HandleAuth(
        ICommandHandler<Authorize.Command, Authorize.Data> handler,
        [FromBody] Authorize.Command command)
        => TypedResults.Ok(await handler.Handle(command));
}