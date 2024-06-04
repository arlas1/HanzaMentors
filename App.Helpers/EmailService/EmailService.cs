using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;

namespace App.Helpers.EmailService;

public class EmailService : IEmailService
{
    private readonly int? SmtpPort = 587;
    private const string? SmtpServer = "smtp.gmail.com";
    private const string? SmtpUsername = "lasimer0406@gmail.com";
    private const string? SmtpPassword = "wzrg xdwn iaxs bhcs";
    private const string? SenderName = "Artur Lasimer";
    private const string? SenderEmail = "lasimer0406@gmail.com";
    
    private const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=[]{}|;:,.<>?";

    public async Task SendEmailAsync(string email, string subject, string body)
    {
        var smtpClient = new SmtpClient
        {
            Host = SmtpServer!,
            Port = (int)SmtpPort!,
            EnableSsl = true,
            Credentials = new NetworkCredential(SmtpUsername, SmtpPassword)
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(SenderEmail!, SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage);
    }

    public string GenerateAccountEmailBody(string firstName, string email, string password)
    {
        return $"Dear {firstName},<br /><br />" +
               $"Your account has been successfully created. The password can be changed later in our application.<br />" +
               $"Username: {email}<br />" +
               $"Password: {password}<br /><br />" +
               $"Best Regards,<br />" +
               $"Artur Lasimer";

    }
    
    public string GenerateDocumentEmailBody(string fullName)
    {
        return $"Dear {fullName},<br/><br/>" +
               $"New documents are awaiting signing. In our application you can choose the way, date and time of signing that is convenient for you.<br/><br/>" +
               $"Best Regards,<br/>" +
               $"Artur Lasimer";

    }
    
    public string GenerateDocSignEmailBody(string title, string time, string fullName)
    {
        return $"Mentee {fullName} have chosen signing time ({time}) for a document {title}.";

    }
    public string GenerateUserPassword(int length = 12)
    {
        char[] chars = new char[length];
        byte[] data = new byte[length];

        using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
        {
            crypto.GetBytes(data);
        }

        Random random = new Random();
        bool hasUppercase = false;
        bool hasNumber = false;
        bool hasSpecialChar = false;

        for (int i = 0; i < length; i++)
        {
            chars[i] = AllowedChars[data[i] % AllowedChars.Length];

            if (!hasUppercase && char.IsUpper(chars[i]))
            {
                hasUppercase = true;
            }

            if (!hasNumber && char.IsDigit(chars[i]))
            {
                hasNumber = true;
            }

            if (!hasSpecialChar && !char.IsLetterOrDigit(chars[i]))
            {
                hasSpecialChar = true;
            }
        }

        if (!hasUppercase || !hasNumber || !hasSpecialChar)
        {
            char[] requiredChars = new char[3];
            requiredChars[0] = AllowedChars[random.Next(26, 52)];
            requiredChars[1] = AllowedChars[random.Next(52, 62)];
            requiredChars[2] = AllowedChars[random.Next(62, AllowedChars.Length)];

            for (int i = 0; i < 3; i++)
            {
                int randomIndex = random.Next(0, length);
                chars[randomIndex] = requiredChars[i];
            }
        }

        return new string(chars);
    }
}