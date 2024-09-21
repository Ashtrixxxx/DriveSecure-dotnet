using Backend.Dto;
using Backend.Models;

namespace Backend.Repository
{
    public interface IUserServices
    {

        Task<UserDetails> GetUserByUserName(string username);
        Task<UserDetails> CreateUser(UserDetails userDetails);

        Task<UserDetails> UpdateUser(UserDetails userDetails);

        Task<IEnumerable<InsurancePolicies>> UserPolicyDetails(int UserId);

        Task OnPaymentCompletion(int userId,VehicleDetails VDetails, InsurancePolicies PolicyDetails, PaymentDetails PaymentDetails, SupportDocuments supportDocuments);

        Task SimpleTestEmail(int id);
    }
}
