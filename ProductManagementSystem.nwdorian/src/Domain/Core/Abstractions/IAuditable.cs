namespace Domain.Core.Abstractions;

public interface IAuditable
{
    DateTime CreatedOnUtc { get; }
    DateTime? UpdatedOnUtc { get; }
    Guid CreatedBy { get; }
    Guid? UpdatedBy { get; }
}
