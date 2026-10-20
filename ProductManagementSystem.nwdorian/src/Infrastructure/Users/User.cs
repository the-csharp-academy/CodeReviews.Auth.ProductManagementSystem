using Domain.Core.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Users;

public class User : IdentityUser<Guid>, IAuditable, ISoftDeletable
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid? DeletedBy { get; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? UpdatedOnUtc { get; }
    public Guid CreatedBy { get; }
    public Guid? UpdatedBy { get; }
}
