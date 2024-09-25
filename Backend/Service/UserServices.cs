using Backend.Dto;
using Backend.Models;
using Backend.Repository;
using MailKit.Security;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MimeKit.Text;
using MimeKit;
using Newtonsoft.Json;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Backend.Service
{
    public class UserServices : IUserServices
    {
        private readonly DriveDbContext _context;
        private readonly IVehicleServices _vehicleServices;
        private readonly IPolicyServices _policyServices;
        private readonly IPaymentServices _paymentServices;
        private readonly ISupportDocumentServices _supportDocumentServices;
        private readonly IEmailService _emailService;

        public UserServices(DriveDbContext cont , IEmailService emailService, IPolicyServices policyServices,IVehicleServices vehicleServices,ISupportDocumentServices supportDocumentServices) {

            _vehicleServices = vehicleServices;
            _supportDocumentServices = supportDocumentServices;
            _policyServices = policyServices;
            _emailService = emailService;
        
            _context = cont;
        }

        public async Task<UserDetails> CreateUser(UserDetails user)
        {
            _context.UserDetails.Add(user);
            await _context.SaveChangesAsync();  
            return user;

        }

        public async Task<UserDetails> UpdateUser(UserDetails user)
        {

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;

        }

        public async Task<IEnumerable<InsurancePolicies>> UserPolicyDetails(int UserId)
        {
            return await _context.InsurancePolicies
                                            .Where(p => p.UserDetails.UserID == UserId)
                                            .ToListAsync();
        }

        public async Task<UserDetails> GetUserById(int UserId)
        {
            return await _context.UserDetails.FindAsync(UserId);
        }

        public async Task<UserDetails> GetUserByUserName(string username)
        {
            return await _context.UserDetails.FirstOrDefaultAsync(p => p.UserName == username);
        }


        public async Task OnFormSubmission(int UserId,VehicleDetails VDetails, InsurancePolicies PolicyDetails, SupportDocuments supportDocuments)
        {

            try
            {
                PolicyDetails.UserID = UserId;
                InsurancePolicies returned_policy = await _policyServices.CreatePolicy(PolicyDetails);



                VDetails.UserID = UserId;
                VDetails.PolicyID = returned_policy.PolicyID;


                await _vehicleServices.CreateVehicle(VDetails);


                supportDocuments.PolicyID = returned_policy.PolicyID;
                supportDocuments.UserID = UserId;

                SupportDocuments docs = await _supportDocumentServices.AddSupportDocument(supportDocuments);
            }catch(Exception e)
            {
                // Rollback transaction in case of error
                throw;
            }

            
    }


        public async Task SimpleTestEmail(int id)
        {
            var user =  await _context.UserDetails.FindAsync(id);
            var myemail = user.Email;

            var subject = "Basic";

            var body = "hellon helloo";
            

            await _emailService.SendEmailAsync(myemail, subject, body);
        }

    }
}
