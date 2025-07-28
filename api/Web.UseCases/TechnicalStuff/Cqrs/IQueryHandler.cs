namespace Web.UseCases.TechnicalStuff.Cqrs;

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery
{
    Task<TResult> Handle(TQuery query);
}