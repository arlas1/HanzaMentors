namespace App.Helpers.EmailService;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string body);
    string GenerateAccountEmailBody(string firstName, string email, string password);
    public string GenerateDocumentEmailBody(string fullName);
    string GenerateUserPassword(int length = 12);
}