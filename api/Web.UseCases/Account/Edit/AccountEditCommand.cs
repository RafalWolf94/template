using JetBrains.Annotations;
using Web.UseCases.TechnicalStuff.Cqrs;

namespace Web.UseCases.Account.Edit;

[PublicAPI]
public sealed record AccountEdit
{
    public record Command(
        string FirstName,
        string LastName,
        string City,
        string PhoneNumberPrefix,
        string PhoneNumber) : ICommand;
}