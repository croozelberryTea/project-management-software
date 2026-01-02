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
        var htmlBody = CreateConfirmationEmailHtml(confirmationLink);
        
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.FromDisplayName, _settings.FromAddress));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "Email Confirmation";
        message.Body = new TextPart("html") { Text = htmlBody };
        
        await SendEmail(message);
    }

    public async Task SendPasswordResetLinkAsync(TUser user, string email, string resetLink)
    {
        var htmlBody = CreatePasswordResetEmailHtml(resetLink);
        
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.FromDisplayName, _settings.FromAddress));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "Password Reset";
        message.Body = new TextPart("html") { Text = htmlBody };
        
        await SendEmail(message);
    }

    public async Task SendPasswordResetCodeAsync(TUser user, string email, string resetCode)
    {
        var htmlBody = CreatePasswordResetCodeEmailHtml(resetCode);
        
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.FromDisplayName, _settings.FromAddress));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "Password Reset Code";
        message.Body = new TextPart("html") { Text = htmlBody };
        
        await SendEmail(message);
    }

    private string GetCommonEmailStyles()
    {
        return @"
        body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }
        .container { max-width: 600px; margin: 0 auto; padding: 20px; }
        .header { color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }
        .content { background-color: #f9f9f9; padding: 30px; border: 1px solid #ddd; }
        .button { display: inline-block; padding: 12px 24px; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0; }
        .warning { padding: 15px; margin: 20px 0; }
        .footer { text-align: center; padding: 20px; color: #666; font-size: 12px; }";
    }

    private string CreateConfirmationEmailHtml(string confirmationLink)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <style>
        {GetCommonEmailStyles()}
        .header {{ background-color: #007bff; }}
        .button {{ background-color: #007bff; }}
        .warning {{ background-color: #fff3cd; border-left: 4px solid #ffc107; }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>Email Confirmation</h1>
        </div>
        <div class=""content"">
            <p>Thank you for registering. Please confirm your email address by clicking the button below:</p>
            
            <div style=""text-align: center;"">
                <a href=""{confirmationLink}"" class=""button"">Confirm Email Address</a>
            </div>
            
            <p>Or copy and paste this link into your browser:</p>
            <p style=""word-break: break-all; background-color: #eee; padding: 10px; border-radius: 3px;"">{confirmationLink}</p>
            
            <div class=""warning"">
                <strong>⚠️ Security Notice:</strong>
                <ul>
                    <li>This link will expire in 24 hours</li>
                    <li>Do not share this link with anyone</li>
                    <li>If you didn't request this email, please ignore it</li>
                    <li>For security, this link can only be used once</li>
                </ul>
            </div>
        </div>
        <div class=""footer"">
            <p>This is an automated message, please do not reply to this email.</p>
        </div>
    </div>
</body>
</html>";
    }

    private string CreatePasswordResetEmailHtml(string resetLink)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <style>
        {GetCommonEmailStyles()}
        .header {{ background-color: #dc3545; }}
        .button {{ background-color: #dc3545; }}
        .warning {{ background-color: #f8d7da; border-left: 4px solid #dc3545; }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>Password Reset Request</h1>
        </div>
        <div class=""content"">
            <p>You have requested to reset your password. Click the button below to proceed:</p>
            
            <div style=""text-align: center;"">
                <a href=""{resetLink}"" class=""button"">Reset Password</a>
            </div>
            
            <p>Or copy and paste this link into your browser:</p>
            <p style=""word-break: break-all; background-color: #eee; padding: 10px; border-radius: 3px;"">{resetLink}</p>
            
            <div class=""warning"">
                <strong>⚠️ Security Notice:</strong>
                <ul>
                    <li>This link will expire in 1 hour</li>
                    <li>Never share this link with anyone - it grants access to reset your password</li>
                    <li>If you didn't request a password reset, ignore this email and your password will remain unchanged</li>
                    <li>For security, this link can only be used once</li>
                    <li>After using this link, your previous password will no longer work</li>
                </ul>
            </div>
            
            <p><strong>Didn't request this?</strong> If you didn't request a password reset, please contact support immediately as someone may be trying to access your account.</p>
        </div>
        <div class=""footer"">
            <p>This is an automated message, please do not reply to this email.</p>
        </div>
    </div>
</body>
</html>";
    }

    private string CreatePasswordResetCodeEmailHtml(string resetCode)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <style>
        {GetCommonEmailStyles()}
        .header {{ background-color: #dc3545; }}
        .code {{ font-size: 32px; font-weight: bold; text-align: center; background-color: #eee; padding: 20px; margin: 20px 0; border-radius: 5px; letter-spacing: 5px; font-family: monospace; }}
        .warning {{ background-color: #f8d7da; border-left: 4px solid #dc3545; }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>Password Reset Code</h1>
        </div>
        <div class=""content"">
            <p>You have requested to reset your password. Use the code below to complete the process:</p>
            
            <div class=""code"">{resetCode}</div>
            
            <div class=""warning"">
                <strong>⚠️ Security Notice:</strong>
                <ul>
                    <li>This code will expire in 15 minutes</li>
                    <li>Never share this code with anyone - it grants access to reset your password</li>
                    <li>Enter this code only on the official password reset page</li>
                    <li>If you didn't request a password reset, ignore this email and your password will remain unchanged</li>
                    <li>For security, this code can only be used once</li>
                </ul>
            </div>
            
            <p><strong>Didn't request this?</strong> If you didn't request a password reset, please contact support immediately as someone may be trying to access your account.</p>
        </div>
        <div class=""footer"">
            <p>This is an automated message, please do not reply to this email.</p>
        </div>
    </div>
</body>
</html>";
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