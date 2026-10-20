using Application.Users;
using Domain.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Database.Interceptors;

public class UpdateAuditableInterceptor(IDateTimeProvider dateTimeProvider, IUserContext userContext)
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
            UpdateAuditableEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditableEntities(DbContext dbContext)
    {
        List<EntityEntry<IAuditable>> entries = dbContext.ChangeTracker.Entries<IAuditable>().ToList();

        foreach (EntityEntry<IAuditable> entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.SetPropertyValue(nameof(IAuditable.CreatedOnUtc), dateTimeProvider.UtcNow);
                entry.SetPropertyValue(nameof(IAuditable.CreatedBy), userContext.UserId);
            }

            if (entry.State == EntityState.Modified)
            {
                entry.SetPropertyValue(nameof(IAuditable.UpdatedOnUtc), dateTimeProvider.UtcNow);
                entry.SetPropertyValue(nameof(IAuditable.UpdatedBy), userContext.UserId);
            }
        }
    }
}
