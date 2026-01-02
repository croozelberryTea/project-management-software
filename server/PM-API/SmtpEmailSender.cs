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
        var plainBody = CreateConfirmationEmailPlainText(confirmationLink);
        
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.FromDisplayName, _settings.FromAddress));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "Email Confirmation";
        
        var multipart = new Multipart("alternative")
        {
            new TextPart("plain") { Text = plainBody },
            new TextPart("html") { Text = htmlBody }
        };
        message.Body = multipart;
        
        await SendEmail(message);
    }

    public async Task SendPasswordResetLinkAsync(TUser user, string email, string resetLink)
    {
        var htmlBody = CreatePasswordResetEmailHtml(resetLink);
        var plainBody = CreatePasswordResetEmailPlainText(resetLink);
        
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.FromDisplayName, _settings.FromAddress));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "Password Reset";
        
        var multipart = new Multipart("alternative")
        {
            new TextPart("plain") { Text = plainBody },
            new TextPart("html") { Text = htmlBody }
        };
        message.Body = multipart;
        
        await SendEmail(message);
    }

    public async Task SendPasswordResetCodeAsync(TUser user, string email, string resetCode)
    {
        var htmlBody = CreatePasswordResetCodeEmailHtml(resetCode);
        var plainBody = CreatePasswordResetCodeEmailPlainText(resetCode);
        
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.FromDisplayName, _settings.FromAddress));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "Password Reset Code";
        
        var multipart = new Multipart("alternative")
        {
            new TextPart("plain") { Text = plainBody },
            new TextPart("html") { Text = htmlBody }
        };
        message.Body = multipart;
        
        await SendEmail(message);
    }

    private string CreateConfirmationEmailHtml(string confirmationLink)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
</head>
<body style=""font-family: Arial, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0;"">
    <div style=""max-width: 600px; margin: 0 auto; padding: 20px;"">
        <div style=""background-color: #007bff; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0;"">
            <h1 style=""margin: 0;"">Email Confirmation</h1>
        </div>
        <div style=""background-color: #f9f9f9; padding: 30px; border: 1px solid #ddd;"">
            <p>Thank you for registering. Please confirm your email address by clicking the button below:</p>
            
            <div style=""text-align: center;"">
                <a href=""{confirmationLink}"" style=""display: inline-block; padding: 12px 24px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0;"">Confirm Email Address</a>
            </div>
            
            <p>Or copy and paste this link into your browser:</p>
            <p style=""word-break: break-all; background-color: #eee; padding: 10px; border-radius: 3px;"">{confirmationLink}</p>
            
            <div style=""background-color: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0;"">
                <strong>⚠️ Security Notice:</strong>
                <ul style=""margin: 10px 0; padding-left: 20px;"">
                    <li>This link will expire in 24 hours</li>
                    <li>Do not share this link with anyone</li>
                    <li>If you didn't request this email, please ignore it</li>
                    <li>For security, this link can only be used once</li>
                </ul>
            </div>
        </div>
        <div style=""text-align: center; padding: 20px; color: #666; font-size: 12px;"">
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
</head>
<body style=""font-family: Arial, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0;"">
    <div style=""max-width: 600px; margin: 0 auto; padding: 20px;"">
        <div style=""background-color: #dc3545; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0;"">
            <h1 style=""margin: 0;"">Password Reset Request</h1>
        </div>
        <div style=""background-color: #f9f9f9; padding: 30px; border: 1px solid #ddd;"">
            <p>You have requested to reset your password. Click the button below to proceed:</p>
            
            <div style=""text-align: center;"">
                <a href=""{resetLink}"" style=""display: inline-block; padding: 12px 24px; background-color: #dc3545; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0;"">Reset Password</a>
            </div>
            
            <p>Or copy and paste this link into your browser:</p>
            <p style=""word-break: break-all; background-color: #eee; padding: 10px; border-radius: 3px;"">{resetLink}</p>
            
            <div style=""background-color: #f8d7da; border-left: 4px solid #dc3545; padding: 15px; margin: 20px 0;"">
                <strong>⚠️ Security Notice:</strong>
                <ul style=""margin: 10px 0; padding-left: 20px;"">
                    <li>This link will expire in 1 hour</li>
                    <li>Never share this link with anyone - it grants access to reset your password</li>
                    <li>If you didn't request a password reset, ignore this email and your password will remain unchanged</li>
                    <li>For security, this link can only be used once</li>
                    <li>After using this link, your previous password will no longer work</li>
                </ul>
            </div>
            
            <p><strong>Didn't request this?</strong> If you didn't request a password reset, please contact support immediately as someone may be trying to access your account.</p>
        </div>
        <div style=""text-align: center; padding: 20px; color: #666; font-size: 12px;"">
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
</head>
<body style=""font-family: Arial, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0;"">
    <div style=""max-width: 600px; margin: 0 auto; padding: 20px;"">
        <div style=""background-color: #dc3545; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0;"">
            <h1 style=""margin: 0;"">Password Reset Code</h1>
        </div>
        <div style=""background-color: #f9f9f9; padding: 30px; border: 1px solid #ddd;"">
            <p>You have requested to reset your password. Use the code below to complete the process:</p>
            
            <div style=""font-size: 32px; font-weight: bold; text-align: center; background-color: #eee; padding: 20px; margin: 20px 0; border-radius: 5px; letter-spacing: 5px; font-family: monospace;"">{resetCode}</div>
            
            <div style=""background-color: #f8d7da; border-left: 4px solid #dc3545; padding: 15px; margin: 20px 0;"">
                <strong>⚠️ Security Notice:</strong>
                <ul style=""margin: 10px 0; padding-left: 20px;"">
                    <li>This code will expire in 15 minutes</li>
                    <li>Never share this code with anyone - it grants access to reset your password</li>
                    <li>Enter this code only on the official password reset page</li>
                    <li>If you didn't request a password reset, ignore this email and your password will remain unchanged</li>
                    <li>For security, this code can only be used once</li>
                </ul>
            </div>
            
            <p><strong>Didn't request this?</strong> If you didn't request a password reset, please contact support immediately as someone may be trying to access your account.</p>
        </div>
        <div style=""text-align: center; padding: 20px; color: #666; font-size: 12px;"">
            <p>This is an automated message, please do not reply to this email.</p>
        </div>
    </div>
</body>
</html>";
    }

    private string CreateConfirmationEmailPlainText(string confirmationLink)
    {
        return $@"EMAIL CONFIRMATION

Thank you for registering. Please confirm your email address by visiting the link below:

{confirmationLink}

SECURITY NOTICE:
- This link will expire in 24 hours
- Do not share this link with anyone
- If you didn't request this email, please ignore it
- For security, this link can only be used once

---
This is an automated message, please do not reply to this email.";
    }

    private string CreatePasswordResetEmailPlainText(string resetLink)
    {
        return $@"PASSWORD RESET REQUEST

You have requested to reset your password. Visit the link below to proceed:

{resetLink}

SECURITY NOTICE:
- This link will expire in 1 hour
- Never share this link with anyone - it grants access to reset your password
- If you didn't request a password reset, ignore this email and your password will remain unchanged
- For security, this link can only be used once
- After using this link, your previous password will no longer work

Didn't request this? If you didn't request a password reset, please contact support immediately as someone may be trying to access your account.

---
This is an automated message, please do not reply to this email.";
    }

    private string CreatePasswordResetCodeEmailPlainText(string resetCode)
    {
        return $@"PASSWORD RESET CODE

You have requested to reset your password. Use the code below to complete the process:

{resetCode}

SECURITY NOTICE:
- This code will expire in 15 minutes
- Never share this code with anyone - it grants access to reset your password
- Enter this code only on the official password reset page
- If you didn't request a password reset, ignore this email and your password will remain unchanged
- For security, this code can only be used once

Didn't request this? If you didn't request a password reset, please contact support immediately as someone may be trying to access your account.

---
This is an automated message, please do not reply to this email.";
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