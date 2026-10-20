using Application.Emails;
using Application.Notifications;

namespace Infrastructure.Notifications;

public class EmailNotificationService(IEmailService emailService) : IEmailNotificationService
{
    public async Task SendPasswordResetEmail(PasswordResetEmail passwordResetEmail)
    {
        EmailRequest emailRequest = new(
            passwordResetEmail.EmailTo,
            "Reset password link",
            $"Please reset your password by <a href='{passwordResetEmail.CallbackUrl}'>clicking here</a>."
        );

        await emailService.SendEmail(emailRequest);
    }

    public async Task SendRegisterConfirmationEmail(RegisterConfirmationEmail registerConfirmationEmail)
    {
        EmailRequest emailRequest = new(
            registerConfirmationEmail.EmailTo,
            "Email confirmation link",
            $"Please confirm your account by <a href='{registerConfirmationEmail.CallbackUrl}'>clicking here</a>."
        );

        await emailService.SendEmail(emailRequest);
    }

    public async Task SendWelcomeEmail(WelcomeEmail welcomeEmail)
    {
        EmailRequest emailRequest = new(
            welcomeEmail.EmailTo,
            "Welcome to Games Edge!",
            $"Welcome to Games Edge, <br><br>you have successfully registered with {welcomeEmail.EmailTo} email."
        );

        await emailService.SendEmail(emailRequest);
    }
}
