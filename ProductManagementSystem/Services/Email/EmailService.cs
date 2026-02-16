using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using ProductManagementSystem.DTO.Email;

namespace ProductManagementSystem.Services.Email;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(
        EmailRequest request,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(request.To))
            throw new ArgumentException("Email recipient (To) is required.", nameof(request));

        var smtpHost =
            _configuration["Email:Smtp:Host"]
            ?? throw new InvalidOperationException("Email:Smtp:Host is not configured.");
        var smtpPortValue = _configuration["Email:Smtp:Port"] ?? "587";
        var smtpUser =
            _configuration["Email:Smtp:Username"]
            ?? throw new InvalidOperationException("Email:Smtp:Username is not configured.");
        var smtpPass =
            _configuration["Email:Smtp:Password"]
            ?? throw new InvalidOperationException("Email:Smtp:Password is not configured.");
        var fromAddress = _configuration["Email:From:Address"] ?? smtpUser;
        var fromName = _configuration["Email:From:Name"] ?? "Product Management System";

        _ = int.TryParse(smtpPortValue, out var smtpPort);
        if (smtpPort <= 0)
            smtpPort = 587;

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(fromName, fromAddress));
        email.To.Add(MailboxAddress.Parse(request.To));
        email.Subject = request.Subject ?? string.Empty;
        email.Body = new TextPart(TextFormat.Html) { Text = request.Body ?? string.Empty };

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            smtpHost,
            smtpPort,
            SecureSocketOptions.StartTls,
            cancellationToken
        );
        await smtp.AuthenticateAsync(smtpUser, smtpPass, cancellationToken);
        await smtp.SendAsync(email, cancellationToken);
        await smtp.DisconnectAsync(true, cancellationToken);
    }
}