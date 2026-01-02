using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using PM_API.Configuration;

namespace PM_API;

public class SmtpEmailSender<TUser>(IOptions<EmailSettings> options) : Microsoft.AspNetCore.Identity.IEmailSender<TUser>
    where TUser : class
{
    private readonly EmailSettings _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));

    public async Task SendConfirmationLinkAsync(TUser user, string email, string confirmationLink)
    {
        var body = $"Confirmation link: {confirmationLink}";
        
        using var message = new MailMessage
        {
            From = new MailAddress(_settings.FromAddress, _settings.FromDisplayName),
            Subject = "Email Confirmation",
            Body = body,
            IsBodyHtml = false
        };
        message.To.Add(new MailAddress(email));
        
        await SendEmail(message);
    }

    public async Task SendPasswordResetLinkAsync(TUser user, string email, string resetLink)
    {
        var body = $"Password reset link: {resetLink}";
        
        using var message = new MailMessage
        {
            From = new MailAddress(_settings.FromAddress, _settings.FromDisplayName),
            Subject = "Password Reset",
            Body = body,
            IsBodyHtml = false
        };
        message.To.Add(new MailAddress(email));
        
        await SendEmail(message);
    }

    public async Task SendPasswordResetCodeAsync(TUser user, string email, string resetCode)
    {
        var body = $"Password reset code: {resetCode}";
        
        using var message = new MailMessage
        {
            From = new MailAddress(_settings.FromAddress, _settings.FromDisplayName),
            Subject = "Password Reset Code",
            Body = body,
            IsBodyHtml = false
        };
        message.To.Add(new MailAddress(email));
        
        await SendEmail(message);
    }

    private async Task SendEmail(MailMessage message)
    {
        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl = _settings.EnableSsl
        };

        if (!string.IsNullOrEmpty(_settings.UserName))
        {
            client.Credentials = new NetworkCredential(_settings.UserName, _settings.Password);
        }

        await client.SendMailAsync(message);
    }
}