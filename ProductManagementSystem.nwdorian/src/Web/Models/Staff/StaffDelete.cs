using System.ComponentModel.DataAnnotations;
using Infrastructure.Users;

namespace Web.Models.Staff;

public class StaffDelete
{
    public Guid Id { get; set; }

    [Display(Name = "First name")]
    public string? FirstName { get; set; }

    [Display(Name = "Last name")]
    public string? LastName { get; set; }
    public string? Email { get; set; }

    public static StaffDelete Empty =>
        new()
        {
            Id = Guid.Empty,
            FirstName = string.Empty,
            LastName = string.Empty,
            Email = string.Empty,
        };

    public static StaffDelete Create(User user)
    {
        return new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
        };
    }
}
