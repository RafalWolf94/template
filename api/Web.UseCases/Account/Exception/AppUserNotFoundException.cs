using Web.Domain.TechnicalStuff.Exceptions;

namespace Web.UseCases.Account.Exception;

public class AppUserNotFoundException : NotFoundException
{
    public static int ErrorCode => 1004;
    public static readonly IReadOnlyDictionary<string, string> InvalidCredentialsError = new Dictionary<string, string>
    {
        { "pl", "Nie znaleziono użytkownika" },
        { "en", "User not found" }
    };

    public override int GetErrorCode() => ErrorCode;

    public override IReadOnlyDictionary<string, string> GetErrorDescriptions()
    {
        return InvalidCredentialsError;
    }
}