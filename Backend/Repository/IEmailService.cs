namespace Backend.Repository
{
    public interface IEmailService
    {

        Task SendEmailAsync(string to, string subject, string body);
        Task SendResetPasswordEmail(string email, string resetUrl);

        void SendExpiryNotification(string toEmail, string policyDetails);
    }
}
