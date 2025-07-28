using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Web.Adapters.Out.AppUsers.Get;
using Web.UseCases.Account.Create;
using Web.UseCases.Account.Delete;
using Web.UseCases.Account.Edit;
using Web.UseCases.TechnicalStuff.Cqrs;

namespace Web.Presentation.Account;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this IEndpointRouteBuilder group)
    {
        var accountGroup = group.MapGroup("account")
            .WithTags("Account")
            .WithOpenApi();

        accountGroup.MapGet("/", HandleGet)
            .RequireAuthorization()
            .Produces<Ok<GetAppUser.Data>>()
            .WithOpenApi();

        accountGroup.MapPut("/", HandleEdit)
            .RequireAuthorization()
            .Produces<Ok>()
            .Accepts<AccountEdit.Command>("application/json")
            .WithOpenApi();

        accountGroup.MapDelete("/", HandleDelete)
            .RequireAuthorization()
            .Produces<Ok>()
            .Accepts<AccountDelete.Command>("application/json")
            .WithOpenApi();
    }


    private static async Task<IResult> HandleGet(
        IQueryHandler<GetAppUser.Query, GetAppUser.Data> query)
    {
        return TypedResults.Ok(await query.Handle(new GetAppUser.Query()));
    }

    private static async Task<IResult> HandleCreate(
        ICommandHandler<AccountCreate.Command> handler,
        [FromBody] AccountCreate.Command command)
    {
        await handler.Handle(command);
        return TypedResults.Ok();
    }

    private static async Task<IResult> HandleEdit(
        ICommandHandler<AccountEdit.Command> handler,
        [FromBody] AccountEdit.Command command)
    {
        await handler.Handle(command);
        return TypedResults.Ok();
    }

    private static async Task<IResult> HandleDelete(
        ICommandHandler<AccountDelete.Command> handler)
    {
        await handler.Handle(new AccountDelete.Command());
        return TypedResults.Ok();
    }
}