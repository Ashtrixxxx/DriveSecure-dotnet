using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.Service
{
    public class UserServices : IUserServices
    {
        private readonly DriveDbContext _context;
        private readonly IVehicleServices _vehicleServices;
        private readonly IPolicyServices _policyServices;
        private readonly IPaymentServices _paymentServices;
        private readonly ISupportDocumentServices _supportDocumentServices;

        public UserServices(DriveDbContext cont) { 
        
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


        public async Task OnPaymentCompletion(VehicleDetails VDetails, InsurancePolicies PolicyDetails, PaymentDetails PaymentDetails, SupportDocuments supportDocuments)
        {
            _vehicleServices.CreateVehicle(VDetails);
            _policyServices.CreatePolicy(PolicyDetails);
            _paymentServices.AddPaymentDetails(PaymentDetails);
            _supportDocumentServices.AddSupportDocument(supportDocuments);
    }




    }
}
