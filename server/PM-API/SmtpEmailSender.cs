using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using PM_API.Configuration;

namespace PM_API;

public class SmtpEmailSender<TUser>(IOptions<EmailSettings> options) : Microsoft.AspNetCore.Identity.IEmailSender<TUser>
    where TUser : class
{
    private readonly EmailSettings _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));

    public async Task SendConfirmationLinkAsync(TUser user, string email, string confirmationLink)
    {
        var body = $"Confirmation link: {confirmationLink}";
        
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.FromDisplayName, _settings.FromAddress));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "Email Confirmation";
        message.Body = new TextPart("plain") { Text = body };
        
        await SendEmail(message);
    }

    public async Task SendPasswordResetLinkAsync(TUser user, string email, string resetLink)
    {
        var body = $"Password reset link: {resetLink}";
        
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.FromDisplayName, _settings.FromAddress));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "Password Reset";
        message.Body = new TextPart("plain") { Text = body };
        
        await SendEmail(message);
    }

    public async Task SendPasswordResetCodeAsync(TUser user, string email, string resetCode)
    {
        var body = $"Password reset code: {resetCode}";
        
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.FromDisplayName, _settings.FromAddress));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "Password Reset Code";
        message.Body = new TextPart("plain") { Text = body };
        
        await SendEmail(message);
    }

    private async Task SendEmail(MimeMessage message)
    {
        using var client = new SmtpClient();
        
        try
        {
            // Determine the security options based on settings
            var secureSocketOptions = _settings.EnableSsl 
                ? SecureSocketOptions.StartTls 
                : SecureSocketOptions.None;
            
            await client.ConnectAsync(_settings.Host, _settings.Port, secureSocketOptions);

            if (!string.IsNullOrEmpty(_settings.UserName))
            {
                await client.AuthenticateAsync(_settings.UserName, _settings.Password);
            }

            await client.SendAsync(message);
        }
        finally
        {
            if (client.IsConnected)
                await client.DisconnectAsync(true);
        }
    }
}