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

        public async Task<InsurancePolicies> GetPolicyStatus(InsurancePolicies policy, int PolicyId)
        {
            throw new NotImplementedException();
        }

        public async Task OnPaymentCompletion(string VDetails, string PolicyDetails, string PaymentDetails, string supportDocuments)
        {

            var vehicleDetails = JsonConvert.DeserializeObject<VehicleDetails>(VDetails);
            var policyDetails = JsonConvert.DeserializeObject<InsurancePolicies>(PolicyDetails);
            var paymentDetails = JsonConvert.DeserializeObject<PaymentDetails>(PaymentDetails);
            var supportDocs = JsonConvert.DeserializeObject<SupportDocuments>(supportDocuments);

             _vehicleServices.CreateVehicle(vehicleDetails);
            _policyServices.CreatePolicy(policyDetails);
            _paymentServices.AddPaymentDetails(paymentDetails);
            _supportDocumentServices.AddSupportDocument(supportDocs);


        }



    }
}
