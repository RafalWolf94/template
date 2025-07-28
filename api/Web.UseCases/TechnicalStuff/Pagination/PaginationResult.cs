using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace Web.UseCases.TechnicalStuff.Pagination;

public record PaginatedResult<TIn>
{
    [PublicAPI] public IReadOnlyCollection<TIn> Items { get; } = new ReadOnlyCollection<TIn>([]);
    [PublicAPI] public int TotalItemsCount { get; }
    [PublicAPI] public int PageCount { get; }
    [PublicAPI] public int PageNumber { get; }
    [PublicAPI] public int PageSize { get; }
    
    public PaginatedResult(IReadOnlyCollection<TIn> items, int totalItemsCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalItemsCount = totalItemsCount;
        PageCount = CountPages();
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    [JsonConstructor]
    private PaginatedResult(IReadOnlyCollection<TIn> items, int totalItemsCount, int pagesCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalItemsCount = totalItemsCount;
        PageCount = pagesCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    private int CountPages()
        => (int)Math.Ceiling((double)TotalItemsCount / PageSize);
}