namespace Web.UseCases.TechnicalStuff.Cqrs;

public interface IMessageHandler
{
    Task Handle(IMessage message);
}