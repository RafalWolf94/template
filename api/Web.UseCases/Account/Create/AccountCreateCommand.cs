namespace Web.UseCases.Account.Create;

[PublicAPI]
public sealed record AccountCreate
{
    public record Command(
        string Email,
        string Password,
        bool AcceptTerms
    ) : ICommand;
}