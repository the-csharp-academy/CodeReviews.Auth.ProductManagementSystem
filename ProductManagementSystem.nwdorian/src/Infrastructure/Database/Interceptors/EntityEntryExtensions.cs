using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Database.Interceptors;

public static class EntityEntryExtensions
{
    public static void SetPropertyValue<T>(this EntityEntry entry, string propertyName, T value)
    {
        entry.Property(propertyName).CurrentValue = value;
    }
}
