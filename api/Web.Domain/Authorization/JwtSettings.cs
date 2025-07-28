namespace Web.Domain.Authorization;

public class JwtSettings
{
    public required string Key { get; init; }
    public required string AppUserActivateTokenKey { get; init; }
    public required string AppUserPasswordResetTokenKey { get; init; }
    public required string AppUserNewsletterTokenKey { get; init; }
    public required string AppUserInvitationTokenKey { get; init; }
    public required int AppUserTokenDuration { get; set; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required double DurationInMinutes { get; init; }
}