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

        public UserServices(DriveDbContext cont , IEmailService emailService) {
            
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


        public async Task OnPaymentCompletion(int UserId,VehicleDetails VDetails, InsurancePolicies PolicyDetails, PaymentDetails PaymentDetails, SupportDocuments supportDocuments)
        {

            var user = GetUserById(UserId);



            _vehicleServices.CreateVehicle(VDetails);
            _policyServices.CreatePolicy(PolicyDetails);
            _paymentServices.AddPaymentDetails(PaymentDetails);
            _supportDocumentServices.AddSupportDocument(supportDocuments);
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
