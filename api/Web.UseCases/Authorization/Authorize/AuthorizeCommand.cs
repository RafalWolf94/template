namespace Web.UseCases.Authorization.Authorize;

[PublicAPI]
public sealed record Authorize
{
    public record Command(
        string Email,
        string Password) : ICommand;

    public record Data(string CalypsoAccessToken);
}