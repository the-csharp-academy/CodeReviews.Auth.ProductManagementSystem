namespace Domain.Core.Abstractions;

public interface ISoftDeletable
{
    DateTime? DeletedOnUtc { get; }
    bool IsDeleted { get; }
    Guid? DeletedBy { get; }
}
