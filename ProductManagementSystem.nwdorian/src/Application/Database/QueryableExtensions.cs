using System.Linq.Expressions;
using Application.Pagination;

namespace Application.Database;

public static class QueryableExtensions
{
    public static IQueryable<T> ConditionalWhere<T>(
        this IQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> predicate
    )
    {
        if (condition)
        {
            return query.Where(predicate);
        }

        return query;
    }

    public static IQueryable<T> ApplySorting<T>(
        this IQueryable<T> query,
        Expression<Func<T, object>> expression,
        SortDirection sortDirection
    )
    {
        if (sortDirection == SortDirection.Descending)
        {
            return query.OrderByDescending(expression);
        }

        return query.OrderBy(expression);
    }
}
