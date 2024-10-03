using Backend.Repository;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using MailKit.Security;
using MimeKit;
using Humanizer;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Net.Mime;


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


        public async void SendExpiryNotification(string toEmail, string policyDetails)
        {

            using (var client = new SmtpClient(_emailSettings.SmtpServer, int.Parse(_emailSettings.Port)))
            {
                client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.From),
                    Subject = "Insurance Expiry Reminder",
                    Body = $"Dear customer, your insurance policy {policyDetails} is about to expire. Please renew it soon.",
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

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


        public async Task SendEmailWithPdfAsync(string toEmail, string subject, string body, byte[] pdfBytes, string pdfFileName)
        {
            // Set up SMTP client
            using (var client = new SmtpClient(_emailSettings.SmtpServer, int.Parse(_emailSettings.Port)))
            {
                // Set SMTP credentials
                client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                client.EnableSsl = true;

                // Create email message
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.From),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true // Set to true for HTML emails
                };

                // Add recipient
                mailMessage.To.Add(toEmail);

                // Attach PDF to the email
                if (pdfBytes != null && pdfBytes.Length > 0)
                {
                    using (var ms = new MemoryStream(pdfBytes))
                    {
                        var attachment = new Attachment(ms, pdfFileName, MediaTypeNames.Application.Pdf);
                        mailMessage.Attachments.Add(attachment);

                        // Send the email with the attached PDF
                        await client.SendMailAsync(mailMessage);
                    }
                }
                else
                {
                    // Send email without attachment if no PDF is provided
                    await client.SendMailAsync(mailMessage);
                }
            }
        }

        }
}
