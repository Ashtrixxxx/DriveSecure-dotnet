using Backend.Repository;

namespace Backend.Service
{
    public class ReminderService : IReminderService
    {
        private readonly IPolicyServices _services;
        private readonly IEmailService _emailService;

        public ReminderService(IPolicyServices services, IEmailService emailService)
        {
            _services = services;
            _emailService = emailService;
        }
        public async Task SendExpiringPolicyRemindersAsync()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
            DateOnly expirationThreshold = today.AddDays(7);

            var expiringPolicies = await _services.GetPoliciesExpiringSoonAsync(expirationThreshold);

            foreach (var policy in expiringPolicies)
            {
                var userId = policy.UserDetails.UserID;
                var username = policy.UserDetails.UserName;
                var userEmail = policy.UserDetails.Email;
                var user = await _services.GetPolicyDetails(userId);
                if (user != null)
                {
                    var subject = "Your Insurance Policy is Expiring Soon";

                    var body = $"Dear {username},<br/>" +
                               $"Your insurance policy (#{policy.PolicyID}) is expiring on {policy.CoverageEndDate:MM/dd/yyyy}. " +
                               "Please renew it to avoid any coverage lapse.<br/>Thank you!<br/>" +
                               "Best regards,<br/>" +
                               "Drive Secure";

                    await _emailService.SendEmailAsync(userEmail, subject, body);
                }
            }
        }
    }
}
