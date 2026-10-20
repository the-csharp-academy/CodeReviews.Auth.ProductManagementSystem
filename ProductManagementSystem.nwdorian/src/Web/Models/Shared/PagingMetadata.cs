namespace Web.Models.Shared;

public record class PagingMetadata(
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasPreviousPage,
    bool HasNextPage
)
{
    public int StartPage => Math.Max(1, PageNumber - 1);
    public int EndPage => Math.Min(TotalPages, PageNumber + 1);
}
