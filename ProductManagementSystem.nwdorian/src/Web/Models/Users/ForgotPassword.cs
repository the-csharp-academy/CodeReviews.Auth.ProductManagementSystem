using System.ComponentModel.DataAnnotations;

namespace Web.Models.Users;

public class ForgotPassword
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
