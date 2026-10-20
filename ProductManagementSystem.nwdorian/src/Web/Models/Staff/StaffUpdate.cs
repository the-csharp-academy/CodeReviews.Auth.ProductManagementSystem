using System.ComponentModel.DataAnnotations;
using Infrastructure.Users;

namespace Web.Models.Staff;

public class StaffUpdate
{
    [MaxLength(150)]
    [Display(Name = "First name")]
    public string? FirstName { get; set; }

    [MaxLength(150)]
    [Display(Name = "Last name")]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    public static StaffUpdate Empty =>
        new()
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            Email = string.Empty,
        };

    public static StaffUpdate Create(User user)
    {
        return new()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
        };
    }
}
