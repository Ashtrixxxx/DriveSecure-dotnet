using Backend.Models;
using Backend.Repository;

namespace Backend.Service
{
    public class UserServices : IUserServices
    {

        public async Task CreateUser(UserDetails user)
        {

        }

        public async Task UpdateUser(UserDetails user, string email)
        {

        }

        public async Task<InsurancePolicies> GetPolicyStatus(InsurancePolicies policy, int PolicyId)
        {
            throw new NotImplementedException();
        }

        public async Task OnPaymentCompletion(string VDetails, string PolicyDetails, string PaymentDetails, string supportDocuments)
        {
        }

         

    }
}
