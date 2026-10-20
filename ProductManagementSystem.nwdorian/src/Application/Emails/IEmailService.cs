namespace Application.Emails;

public interface IEmailService
{
    Task SendEmail(EmailRequest request);
}
