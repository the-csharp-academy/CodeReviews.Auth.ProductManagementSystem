using System.Linq.Expressions;
using Application.Pagination;
using Domain.Games;

namespace Application.Games.Pagination;

public record class GameSorting(GameSortingColumn SortBy, SortDirection SortDirection)
{
    public Expression<Func<Game, object>> GetSortColumn()
    {
        return SortBy switch
        {
            GameSortingColumn.Name => g => g.Name,
            GameSortingColumn.Genre => g => g.Genre,
            GameSortingColumn.Price => g => g.Price,
            GameSortingColumn.ReleaseDate => g => g.ReleaseDate,
            _ => g => g.Name,
        };
    }
}
