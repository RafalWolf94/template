using Web.Domain.TechnicalStuff.Exceptions;

namespace Web.UseCases.Authorization.Exception;

public class AppUserNotActiveException : BaseException
{
    public static int ErrorCode => 1002;

    public static readonly IReadOnlyDictionary<string, string> InvalidCredentialsError = new Dictionary<string, string>
    {
        { "pl", "Użytkownik nie jest aktywny" },
        { "en", "User is not active" }
    };

    public override int GetErrorCode() => ErrorCode;

    public override IReadOnlyDictionary<string, string> GetErrorDescriptions()
    {
        return InvalidCredentialsError;
    }
}