using Backend.Models;
using Backend.Repository;
using MailKit;
using Microsoft.EntityFrameworkCore;

namespace Backend.Service
{
    public class PolicyServices : IPolicyServices
    {
        private readonly DriveDbContext _driveDbContext;
        private readonly IEmailService _emailService;

        public PolicyServices(DriveDbContext context, IEmailService emailService) {
        
            _driveDbContext = context;
            _emailService = emailService;
        }

       public async Task<InsurancePolicies> CreatePolicy(InsurancePolicies insurancePolicies)
        {
             _driveDbContext.InsurancePolicies.Add(insurancePolicies);
            await _driveDbContext.SaveChangesAsync();
            return insurancePolicies;

        }

        public async Task<IEnumerable<InsurancePolicies>> GetAllPoliciesForAdmin()
        {
            return await _driveDbContext.InsurancePolicies.ToListAsync();

        }


        public async Task<InsurancePolicies> GetPolicyStatus(int PolicyId)
        {
            return await _driveDbContext.InsurancePolicies.FindAsync(PolicyId);
        }

        public async Task<IEnumerable<InsurancePolicies>> GetAllPolicies(int userId)
        {
            return await _driveDbContext.InsurancePolicies
                                        .Where(policy => policy.UserID == userId)
                                        .ToListAsync();
        }

        public async  Task<InsurancePolicies> GetPolicyDetails(int PolicyId)
        {
            return await _driveDbContext.InsurancePolicies.FindAsync(PolicyId);
        }

        public async Task<InsurancePolicies> PolicyAccepted(int PolicyId)
        {
            var policy = await _driveDbContext.InsurancePolicies.FindAsync(PolicyId);

            //Update Status to accepted
            policy.Status = 2;
            await _driveDbContext.SaveChangesAsync();

            //Fetch Related Details
            var user = await _driveDbContext.UserDetails.FindAsync(policy.UserID);
            var vehicleDetails = await _driveDbContext.VehicleDetails
                                      .Where(v => v.PolicyID == PolicyId)
                                      .ToListAsync();

            var paymentDetails = await _driveDbContext.PaymentDetails
                                       .Where(p => p.PolicyID == PolicyId)
                                       .FirstOrDefaultAsync();

            var supportDocuments = await _driveDbContext.SupportDocuments
                                        .Where(s => s.PolicyID == PolicyId && s.UserID == policy.UserID)
                                        .FirstOrDefaultAsync();

            //Composing the Email body

            var emailBody = $"Dear {user.UserName}, \n\n" +
                            "Your insurance policy has been accepted.\n\n" +
                            "Vehicle Details:\n"+
                            $"{string.Join("\n", vehicleDetails.Select(v => $"Model: {v.VehicleModel}, Type: {v.VehicleType}"))}\n\n" +
                            "Payment Details:\n" +
                            $"Amount: {paymentDetails.PremiumAmount}, Date: {paymentDetails.PaymentDate}\n\n" +
                            "Support Documents:\n" +
                            $"Address Proof: {supportDocuments?.AddressProof}\n" +
                            $"RC Proof: {supportDocuments?.RCProof}\n\n" +
                            "Thank you for using our Service!" +
                            "Best regards,<br/>" +
                            "Drive Secure";


            await _emailService.SendEmailAsync(user.Email, "Policy Accepted", emailBody);

            return policy;

        }

        public async Task<InsurancePolicies> PolicyRejected(int PolicyId)
        {
            var s = await _driveDbContext.InsurancePolicies.FindAsync(PolicyId);

            s.Status = 3;

            _driveDbContext.SaveChangesAsync();

            return s;
        }

        public async Task<IEnumerable<InsurancePolicies>> ShowAcceptedPolicies()
        {
            return await _driveDbContext.InsurancePolicies.Where(i => i.Status == 1).ToListAsync();
        }

        public async Task<IEnumerable<InsurancePolicies>> ShowRejectedPolicies()
        {
            return await _driveDbContext.InsurancePolicies.Where(i => i.Status == 2).ToListAsync();
        }

        public async Task<IEnumerable<InsurancePolicies>> GetPoliciesExpiringSoonAsync(DateOnly expirationDate)
        {
            return await _driveDbContext.InsurancePolicies
                .Where(policy => policy.CoverageEndDate <= expirationDate && !policy.IsRenewed)
                .ToListAsync();
        }
    }
}
