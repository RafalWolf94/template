using Web.Domain.TechnicalStuff.Exceptions;

namespace Web.UseCases.Authorization.Exception;

public class InvalidCredentialsException : BaseException
{
    public static int ErrorCode => 1003;
    public static readonly IReadOnlyDictionary<string, string> InvalidCredentialsError = new Dictionary<string, string>
    {
        { "pl", "Błędny Login i/lub hasła" },
        { "en", "Invalid login and/or password" }
    };

    public override int GetErrorCode() => ErrorCode;

    public override IReadOnlyDictionary<string, string> GetErrorDescriptions()
    {
        return InvalidCredentialsError;
    }
}