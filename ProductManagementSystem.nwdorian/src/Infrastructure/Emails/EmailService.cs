using Application.Emails;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Infrastructure.Emails;

public class EmailService(SmtpSettings settings) : IEmailService
{
    public async Task SendEmail(EmailRequest request)
    {
        using MimeMessage message = new();
        message.From.Add(new MailboxAddress(settings.FromName, settings.FromAddress));
        message.To.Add(new MailboxAddress(null, request.EmailTo));
        message.Subject = request.Subject;

        BodyBuilder body = new() { HtmlBody = request.Body };
        message.Body = body.ToMessageBody();

        using SmtpClient client = new();

        await client.ConnectAsync(
            settings.Host,
            settings.Port,
            settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None
        );

        await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }
}
