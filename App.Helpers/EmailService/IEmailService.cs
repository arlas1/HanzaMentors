namespace App.Helpers.EmailService;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string body);
    string GenerateEmailBody(string firstName, string email, string password);
    string GenerateUserPassword(int length = 12);
}