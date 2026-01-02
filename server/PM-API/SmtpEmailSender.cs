using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using PM_API.Configuration;

namespace PM_API;

public class SmtpEmailSender<TUser>(IOptions<EmailSettings> options, ILogger<SmtpEmailSender<TUser>> logger) : Microsoft.AspNetCore.Identity.IEmailSender<TUser>
    where TUser : class
{
    private readonly EmailSettings _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
    private readonly ILogger<SmtpEmailSender<TUser>> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

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
            
            _logger.LogDebug("Connecting to SMTP server {Host}:{Port}", _settings.Host, _settings.Port);
            await client.ConnectAsync(_settings.Host, _settings.Port, secureSocketOptions);

            if (!string.IsNullOrEmpty(_settings.UserName))
            {
                _logger.LogDebug("Authenticating with SMTP server as {UserName}", _settings.UserName);
                await client.AuthenticateAsync(_settings.UserName, _settings.Password);
            }

            _logger.LogDebug("Sending email to {Recipients}", string.Join(", ", message.To));
            await client.SendAsync(message);
            _logger.LogInformation("Email sent successfully to {Recipients} with subject '{Subject}'", 
                string.Join(", ", message.To), message.Subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {Recipients} with subject '{Subject}'. SMTP server: {Host}:{Port}", 
                string.Join(", ", message.To), message.Subject, _settings.Host, _settings.Port);
            throw;
        }
        finally
        {
            if (client.IsConnected)
            {
                _logger.LogDebug("Disconnecting from SMTP server");
                await client.DisconnectAsync(true);
            }
        }
    }
}