using Web.Adapters.Out.AppUsers.Get;
using Web.UseCases.Account.Create;
using Web.UseCases.Account.Delete;
using Web.UseCases.Account.Edit;

namespace Web.Presentation.Account;

public static class AccountEndpoints
{
    private const string AccountGroupEndpoint = "account";

    public static void MapAccountEndpoints(this IEndpointRouteBuilder group)
    {
        var accountGroup = group.MapGroup(AccountGroupEndpoint)
            .WithTags("Account")
            .WithOpenApi();

        accountGroup.MapGet("/", HandleGet)
            .RequireAuthorization()
            .Produces<Ok<GetAccount.Data>>()
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


    private static async Task<IResult> HandleGet(IQueryHandler<GetAccount.Query, GetAccount.Data> query)
    {
        return TypedResults.Ok(await query.Handle(new GetAccount.Query()));
    }

    private static async Task<IResult> HandleCreate(ICommandHandler<AccountCreate.Command> handler, [FromBody] AccountCreate.Command command)
    {
        await handler.Handle(command);
        return TypedResults.Ok();
    }

    private static async Task<IResult> HandleEdit(ICommandHandler<AccountEdit.Command> handler, [FromBody] AccountEdit.Command command)
    {
        await handler.Handle(command);
        return TypedResults.Ok();
    }

    private static async Task<IResult> HandleDelete(ICommandHandler<AccountDelete.Command> handler)
    {
        await handler.Handle(new AccountDelete.Command());
        return TypedResults.Ok();
    }
}