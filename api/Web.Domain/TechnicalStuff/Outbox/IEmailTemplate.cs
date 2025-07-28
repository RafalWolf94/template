using Web.Domain.Models.ValueObjects;

namespace Web.Domain.TechnicalStuff.Outbox;

public interface IEmailTemplate
{
    string Subject { get; set; }
    string Message { get; set; }
    List<Email> Recipients { get; set; }
}