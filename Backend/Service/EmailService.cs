using Backend.Repository;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using MailKit.Security;
using MimeKit;
using Humanizer;

namespace Backend.Service
{
    public class EmailService : IEmailService
    {


        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendResetPasswordEmail(string email, string resetUrl)
        {

            using (var client = new SmtpClient(_emailSettings.SmtpServer, int.Parse(_emailSettings.Port)))
            {
                client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.From),
                    Subject = "Reset Your Password",
                    Body = $"Please reset your password by clicking the following link: {resetUrl}",
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using (var client = new SmtpClient(_emailSettings.SmtpServer, int.Parse(_emailSettings.Port)))
            {
                client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.From),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(to);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
