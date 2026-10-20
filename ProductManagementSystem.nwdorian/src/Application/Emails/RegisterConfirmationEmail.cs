namespace Application.Emails;

public record class RegisterConfirmationEmail(string EmailTo, string CallbackUrl);
