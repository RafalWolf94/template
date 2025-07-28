using JetBrains.Annotations;
using Web.UseCases.TechnicalStuff.Cqrs;

namespace Web.UseCases.Account.Delete;

[PublicAPI]
public sealed record AccountDelete
{
    public record Command : ICommand;
}