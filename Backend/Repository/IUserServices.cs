using Backend.Models;

namespace Backend.Repository
{
    public interface IUserServices
    {

        Task<UserDetails> CreateUser(UserDetails userDetails);

        Task<UserDetails> UpdateUser(UserDetails userDetails);

        Task<IEnumerable<InsurancePolicies>> UserPolicyDetails(int UserId);

        Task OnPaymentCompletion(VehicleDetails VDetails, InsurancePolicies PolicyDetails, PaymentDetails PaymentDetails, SupportDocuments supportDocuments);

    }
}
