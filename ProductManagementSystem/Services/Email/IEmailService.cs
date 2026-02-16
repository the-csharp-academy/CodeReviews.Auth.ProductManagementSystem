using ProductManagementSystem.DTO.Email;

namespace ProductManagementSystem.Services.Email;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequest request, CancellationToken cancellationToken = default);
}