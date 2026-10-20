using Domain.Core.Abstractions;

namespace Domain.Games;

public class Game : ISoftDeletable, IAuditable
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Genre { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid? DeletedBy { get; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? UpdatedOnUtc { get; }
    public Guid CreatedBy { get; }
    public Guid? UpdatedBy { get; }
}
