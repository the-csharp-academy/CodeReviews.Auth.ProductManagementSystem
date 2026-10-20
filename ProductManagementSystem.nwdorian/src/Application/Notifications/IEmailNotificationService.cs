using Application.Emails;

namespace Application.Notifications;

public interface IEmailNotificationService
{
    Task SendPasswordResetEmail(PasswordResetEmail passwordResetEmail);
    Task SendRegisterConfirmationEmail(RegisterConfirmationEmail registerConfirmationEmail);
    Task SendWelcomeEmail(WelcomeEmail welcomeEmail);
}
