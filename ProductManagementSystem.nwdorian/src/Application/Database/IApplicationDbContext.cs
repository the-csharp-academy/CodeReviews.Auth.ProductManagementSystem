using Domain.Games;
using Microsoft.EntityFrameworkCore;

namespace Application.Database;

public interface IApplicationDbContext
{
    DbSet<Game> Games { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
