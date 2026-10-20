using Microsoft.EntityFrameworkCore;

namespace Application.Pagination;

public class PagedList<T>
{
    private PagedList(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public IReadOnlyList<T> Items { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<PagedList<T>> Create(
        IQueryable<T> query,
        Paging paging,
        CancellationToken cancellationToken
    )
    {
        int totalCount = await query.CountAsync(cancellationToken);
        IReadOnlyList<T> items = await query
            .Skip((paging.PageNumber - 1) * paging.PageSize)
            .Take(paging.PageSize)
            .ToListAsync(cancellationToken);

        return new(items, paging.PageNumber, paging.PageSize, totalCount);
    }
}
