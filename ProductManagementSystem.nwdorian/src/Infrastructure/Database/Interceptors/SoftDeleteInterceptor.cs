using Application.Users;
using Domain.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Database.Interceptors;

public class SoftDeleteInterceptor(IDateTimeProvider dateTimeProvider, IUserContext userContext)
    : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        if (eventData.Context is not null)
        {
            SoftDeleteEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void SoftDeleteEntities(DbContext dbContext)
    {
        List<EntityEntry<ISoftDeletable>> entries = dbContext
            .ChangeTracker.Entries<ISoftDeletable>()
            .Where(e => e.State == EntityState.Deleted)
            .ToList();

        foreach (EntityEntry<ISoftDeletable> entry in entries)
        {
            entry.State = EntityState.Modified;
            entry.SetPropertyValue(nameof(ISoftDeletable.IsDeleted), true);
            entry.SetPropertyValue(nameof(ISoftDeletable.DeletedOnUtc), dateTimeProvider.UtcNow);
            entry.SetPropertyValue(nameof(ISoftDeletable.DeletedBy), userContext.UserId);
        }
    }
}
