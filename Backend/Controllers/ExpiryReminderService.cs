using Backend.Models;
using Backend.Repository;
using Backend.Service;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ExpiryReminderService
{
    private readonly IPolicyServices _repository;
    private readonly IEmailService _emailService;
    private readonly IUserServices _userService;


    public ExpiryReminderService(IPolicyServices repository, IEmailService emailService,IUserServices userServices)
    {
        _repository = repository;
        _userService = userServices;
        _emailService = emailService;
    }

    public async Task CheckAndSendReminders()
    {
        // Fetch policies expiring soon asynchronously
        var expiringPolicies = await _repository.GetPoliciesExpiringSoon();

        // Send an email for each policy
        foreach (var policy in expiringPolicies)
        {
            // Assuming policy.UserDetails?.Email is where the user's email is stored
            var userDetails = await _userService.GetUserByUserId(policy.UserID);

            if (userDetails?.Email != null)
            {
                // Send expiry notification to the user's email
                _emailService.SendExpiryNotification(userDetails.Email,
                    $"Policy #{policy.PolicyID} expiring on {policy.CoverageEndDate}");
            }
        }
    }
}
