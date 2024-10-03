using Backend.Models;
using Backend.Repository;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
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

            var paymentDetails = await _driveDbContext.PaymentDetails
                                       .Where(p => p.PolicyID == PolicyId)
                                       .FirstOrDefaultAsync();

            //Update Status to accepted
            policy.Status = 2;
            await _driveDbContext.SaveChangesAsync();

            //Fetch Related Details
            var user = await _driveDbContext.UserDetails.FindAsync(policy.UserID);
            var vehicleDetails = await _driveDbContext.VehicleDetails
                                      .Where(v => v.PolicyID == PolicyId)
                                      .ToListAsync();

            
            var supportDocuments = await _driveDbContext.SupportDocuments
                                        .Where(s => s.PolicyID == PolicyId && s.UserID == policy.UserID)
                                        .FirstOrDefaultAsync();

            //Composing the Email body

            var emailBody = $"Dear {user.UserName}, \n\n" +
                            "Your insurance policy has been accepted.\n\n" +
                            "Please Continue with the payment process to get your insurance"+
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

          await  _driveDbContext.SaveChangesAsync();

            return s;
        }

        public async Task<InsurancePolicies> PolicyPaid(int PolicyId)
        {
            var s = await _driveDbContext.InsurancePolicies.FindAsync(PolicyId);

            s.Status = 4;

          await   _driveDbContext.SaveChangesAsync();


            var policy = await _driveDbContext.InsurancePolicies.FindAsync(PolicyId);

            var paymentDetails = await _driveDbContext.PaymentDetails
                                       .Where(p => p.PolicyID == PolicyId)
                                       .FirstOrDefaultAsync();

            
            var user = await _driveDbContext.UserDetails.FindAsync(policy.UserID);
            var vehicleDetails = await _driveDbContext.VehicleDetails
                                      .Where(v => v.PolicyID == PolicyId)
                                      .ToListAsync();


            var supportDocuments = await _driveDbContext.SupportDocuments
                                        .Where(s => s.PolicyID == PolicyId && s.UserID == policy.UserID)
                                        .FirstOrDefaultAsync();

            var pdfBytes = GeneratePolicyPdf(user, vehicleDetails, paymentDetails, supportDocuments);


            var emailBody = $"Dear {user.UserName}, \n\n" +
                            "Your insurance policy has been accepted.\n\n" +
                            "Vehicle Details:\n" +
                            $"{string.Join("\n", vehicleDetails.Select(v => $"Model: {v.VehicleModel}, Type: {v.VehicleType}"))}\n\n" +
                            "Payment Details:\n" +
                            $"Amount: {paymentDetails?.PremiumAmount}, Date: {paymentDetails?.PaymentDate}\n\n" +
                            "Your insurance has been attached with the email"+
                            "Thank you for using our Service!" +
                            "Best regards,<br/>" +
                            "Drive Secure";


            await _emailService.SendEmailWithPdfAsync(user.Email, "Policy Accepted", emailBody, pdfBytes, "PolicyDetails.pdf");


            return s;
        }

        public byte[] GeneratePolicyPdf(UserDetails user, List<VehicleDetails> vehicleDetails, PaymentDetails paymentDetails, SupportDocuments supportDocuments)
        {
            using (var ms = new MemoryStream())
            {
                // Create a PDF writer to write to the memory stream
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // Add content to the PDF
                document.Add(new Paragraph($"Dear {user.UserName},"));
                document.Add(new Paragraph("Your insurance policy has been accepted."));
                document.Add(new Paragraph("\nVehicle Details:"));
                foreach (var vehicle in vehicleDetails)
                {
                    document.Add(new Paragraph($"Model: {vehicle.VehicleModel}, Type: {vehicle.VehicleType}"));
                }

                document.Add(new Paragraph("\nPayment Details:"));
                document.Add(new Paragraph($"Amount: {paymentDetails?.PremiumAmount}, Date: {paymentDetails?.PaymentDate}"));

                document.Add(new Paragraph("\nSupport Documents:"));
                document.Add(new Paragraph($"Address Proof: {supportDocuments?.AddressProof}"));
                document.Add(new Paragraph($"RC Proof: {supportDocuments?.RCProof}"));

                document.Add(new Paragraph("\nThank you for using our Service!"));
                document.Add(new Paragraph("Best regards,\nDrive Secure"));

                document.Close();

                return ms.ToArray(); // Return the generated PDF as byte array
            }
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


        public async Task<IEnumerable<InsurancePolicies>> GetPoliciesExpiringSoon(int daysBeforeExpiry = 7)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now); // Get current date in DateOnly format
            var targetDate = currentDate.AddDays(daysBeforeExpiry); // Calculate target date by adding 'daysBeforeExpiry'

            // Retrieve policies where CoverageEndDate is within the next 'daysBeforeExpiry'
            

            return await _driveDbContext.InsurancePolicies
                .Where(p => p.CoverageEndDate >= currentDate && p.CoverageEndDate <= targetDate && !p.IsRenewed)
                .ToListAsync(); ;
        }


    }
}
