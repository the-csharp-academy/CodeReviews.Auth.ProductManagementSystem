using Microsoft.AspNetCore.Identity;
using ProductManagementSystem.Data;
using ProductManagementSystem.DTO.Email;

namespace ProductManagementSystem.Services.Email;

public class IdentityEmailSender : IEmailSender<ApplicationUser>
{
    private readonly IEmailService _emailService;

    public IdentityEmailSender(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public Task SendConfirmationLinkAsync(
        ApplicationUser user,
        string email,
        string confirmationLink
    )
    {
        return _emailService.SendEmailAsync(
            new EmailRequest
            {
                To = email,
                Subject = "Confirm your email",
                Body = $"""
                        <p>Hello,</p>
                        <p>Please confirm your account by clicking this link:</p>
                        <p><a href="{confirmationLink}">Confirm email</a></p>
                        <p>If you didn’t request this, you can ignore this email.</p>
                        """
            }
        );
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        return _emailService.SendEmailAsync(
            new EmailRequest
            {
                To = email,
                Subject = "Reset your password",
                Body = $"""
                        <p>Hello,</p>
                        <p>You can reset your password by clicking this link:</p>
                        <p><a href="{resetLink}">Reset password</a></p>
                        <p>If you didn’t request this, you can ignore this email.</p>
                        """
            }
        );
    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        return _emailService.SendEmailAsync(
            new EmailRequest
            {
                To = email,
                Subject = "Your password reset code",
                Body = $"""
                        <p>Your password reset code is:</p>
                        <p><strong>{resetCode}</strong></p>
                        """
            }
        );
    }
}