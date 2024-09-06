using Backend.Models;

namespace Backend.Repository
{
    public interface IUserServices
    {

        Task CreateUser(UserDetails userDetails);

        Task UpdateUser(UserDetails userDetails, string email);

        Task<InsurancePolicies> GetPolicyStatus(InsurancePolicies policy, int PolicyId);

        Task OnPaymentCompletion(string VDetails, string PolicyDetails, string PaymentDetails, string supportDocuments);

    }
}
