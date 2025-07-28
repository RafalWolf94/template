using Web.Domain.TechnicalStuff.Exceptions;

namespace Web.UseCases.Authorization.Exception;

public class PasswordDoesNotMatchException: BaseException
{
    public static int ErrorCode => 1005;
    public static readonly IReadOnlyDictionary<string, string> InvalidCredentialsError = new Dictionary<string, string>
    {
        { "pl", "Podane hasła nie są zgodne" },
        { "en", "Passwords do not match" }
    };

    public override int GetErrorCode() => ErrorCode;

    public override IReadOnlyDictionary<string, string> GetErrorDescriptions()
    {
        return InvalidCredentialsError;
    }
}