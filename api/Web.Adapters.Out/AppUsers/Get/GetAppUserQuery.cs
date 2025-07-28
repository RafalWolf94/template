using JetBrains.Annotations;
using Web.UseCases.TechnicalStuff.Cqrs;

namespace Web.Adapters.Out.AppUsers.Get;

[PublicAPI]
public sealed record GetAppUser
{
    public record Query : IQuery;

    public record Data(
        string Email,
        string FirstName,
        string LastName,
        string City,
        string PhoneNumberPrefix,
        string PhoneNumber,
        bool IsPasswordRandom);
}