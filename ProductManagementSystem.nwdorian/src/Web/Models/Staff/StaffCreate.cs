using System.ComponentModel.DataAnnotations;

namespace Web.Models.Staff;

public class StaffCreate
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

    [Required]
    [StringLength(
        100,
        ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 6
    )]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }
}
