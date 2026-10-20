namespace Domain.Core.Abstractions;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
