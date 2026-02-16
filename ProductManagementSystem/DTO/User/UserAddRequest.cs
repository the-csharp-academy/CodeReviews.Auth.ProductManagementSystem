using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.DTO.User;

public class UserAddRequest
{
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(
        20,
        MinimumLength = 8,
        ErrorMessage = "{0} must be between {1} and {2} characters long."
    )]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Password confirmation is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}