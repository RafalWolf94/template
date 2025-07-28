using Web.Domain.Models.ValueObjects;

namespace Web.Domain.TechnicalStuff.Outbox;

public class EmailTemplate(
    string subject,
    string message,
    List<Email> recipients
) : IEmailTemplate
{
    public string Subject { get; set; } = subject;
    public string Message { get; set; } = message;
    public List<Email> Recipients { get; set; } = recipients;
}