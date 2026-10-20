namespace Application.Emails;

public record class EmailRequest(string EmailTo, string Subject, string Body);
