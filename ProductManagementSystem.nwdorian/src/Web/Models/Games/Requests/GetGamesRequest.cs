using Application.Games.Pagination;
using Application.Pagination;
using Web.Constants;

namespace Web.Models.Games.Requests;

public record class GetGamesRequest(
    string? SearchTerm = null,
    GameSortingColumn SortBy = GameSortingColumn.Name,
    SortDirection SortDirection = SortDirection.Ascending,
    int PageNumber = PagingDefaults.PageNumber,
    int PageSize = PagingDefaults.PageSize
);
