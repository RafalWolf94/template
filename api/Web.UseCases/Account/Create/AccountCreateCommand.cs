using JetBrains.Annotations;
using Web.UseCases.TechnicalStuff.Cqrs;

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