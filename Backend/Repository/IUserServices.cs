using Backend.Models;

namespace Backend.Repository
{
    public interface IUserServices
    {

        Task<UserDetails> CreateUser(UserDetails userDetails);

        Task<UserDetails> UpdateUser(UserDetails userDetails);

        Task<InsurancePolicies> GetPolicyStatus(InsurancePolicies policy, int PolicyId);

        Task OnPaymentCompletion(string VDetails, string PolicyDetails, string PaymentDetails, string supportDocuments);

    }
}
