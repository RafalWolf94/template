using JetBrains.Annotations;

namespace Web.Infrastructure.Services.Onboarding;

[UsedImplicitly]
public class OnboardingSettings
{
    public string Url { get; set; }
    public string InviteUrl { get; set; }
    public string VerifyAccount { get; set; }
    public string PasswordReset { get; set; }
    public string NewsletterUnsubscribe { get; set; }
}