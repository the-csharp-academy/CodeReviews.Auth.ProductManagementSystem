using System.ComponentModel.DataAnnotations;
using Infrastructure.Users;

namespace Web.Models.Staff.Items;

public class StaffItem(User user)
{
    public Guid Id { get; set; } = user.Id;

    [Display(Name = "First name")]
    public string? FirstName { get; set; } = user.FirstName;

    [Display(Name = "Last name")]
    public string? LastName { get; set; } = user.LastName;
    public string? Email { get; set; } = user.Email;
}
