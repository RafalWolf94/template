using Web.UseCases.TechnicalStuff.Pagination;

namespace Web.UseCases.TechnicalStuff.Cqrs;

public interface IPaginatedQueryHandler<in TQuery, TResult> where TQuery : IQuery
{
    Task<PaginatedResult<TResult>> Handle(TQuery query);
}