using Application.Games.Pagination;
using Application.Pagination;

namespace Application.Games.Queries;

public record class GetGamesPageQuery(GameFilter Filter, GameSorting Sorting, Paging Paging);
