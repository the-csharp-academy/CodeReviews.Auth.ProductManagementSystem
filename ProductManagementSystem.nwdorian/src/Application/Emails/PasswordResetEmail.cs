namespace Application.Emails;

public record class PasswordResetEmail(string EmailTo, string CallbackUrl);
