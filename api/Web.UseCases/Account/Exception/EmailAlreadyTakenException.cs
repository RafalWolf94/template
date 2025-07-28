using Web.Domain.TechnicalStuff.Exceptions;

namespace Web.UseCases.Account.Exception;

public class EmailAlreadyTakenException : BaseException
{
    public static int ErrorCode => 1001;
    public static readonly IReadOnlyDictionary<string, string> InvalidCredentialsError = new Dictionary<string, string>
    {
        { "pl", "Adres e-mail aktualnie w użyciu" },
        { "en", "E-mail is already in use" }
    };

    public override int GetErrorCode() => ErrorCode;

    public override IReadOnlyDictionary<string, string> GetErrorDescriptions()
    {
        return InvalidCredentialsError;
    }
}
