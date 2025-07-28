using Web.Domain.TechnicalStuff.Exceptions;

namespace Web.UseCases.Account.Exception;

public class InvalidTokenException : BaseException
{
    public static int ErrorCode => 1005;

    public static readonly IReadOnlyDictionary<string, string> InvalidCredentialsError = new Dictionary<string, string>
    {
        { "pl", "Nieprawidłowy token" },
        { "en", "Invalid token" }
    };

    public override int GetErrorCode() => ErrorCode;

    public override IReadOnlyDictionary<string, string> GetErrorDescriptions()
    {
        return InvalidCredentialsError;
    }
}