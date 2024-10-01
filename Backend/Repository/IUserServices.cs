using Backend.Dto;
using Backend.Models;

namespace Backend.Repository
{
    public interface IUserServices
    {

        Task<UserDetails> GetUserByUserName(string username);

        Task<UserDetails> GetUserByUserId(int userid);

        Task<UserDetails> CreateUser(UserDetails userDetails);

        Task<UserDetails> UpdateUser(UserDetails userDetails);

        Task<IEnumerable<InsurancePolicies>> UserPolicyDetails(int UserId);

        Task OnFormSubmission(int userId,VehicleDetails VDetails, InsurancePolicies PolicyDetails,  SupportDocuments supportDocuments);

        Task SendPasswordResetEmail(string email);
        Task ResetPassword(string token, string newPassword);

        Task SimpleTestEmail(int id);
    }
}
