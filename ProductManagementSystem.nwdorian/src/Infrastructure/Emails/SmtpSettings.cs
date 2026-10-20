using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Emails;

public class SmtpSettings
{
    public const string SectionName = "Smtp";

    [Required(ErrorMessage = "SMTP host is required")]
    public string Host { get; set; } = string.Empty;

    [Range(1, 65535, ErrorMessage = "Port must be between 1 and 65535")]
    public int Port { get; set; }
    public bool EnableSsl { get; set; }

    [Required(ErrorMessage = "From adress is required")]
    public string FromAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "From name is required")]
    public string FromName { get; set; } = string.Empty;
}
