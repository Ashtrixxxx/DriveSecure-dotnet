using Backend.Models;

namespace Backend.Repository
{
    public interface IPolicyServices
    {

        Task<InsurancePolicies> CreatePolicy(InsurancePolicies insurancePolicies);

        Task<IEnumerable<InsurancePolicies>> GetAllPolicies(int UserId);

        Task <InsurancePolicies> GetPolicyDetails(int PolicyId);

        Task<InsurancePolicies> GetPolicyStatus(int PolicyId);

        Task<InsurancePolicies> PolicyAccepted(int PolicyId);

        Task<InsurancePolicies> PolicyRejected(int PolicyId);

        Task<IEnumerable<InsurancePolicies>> ShowAcceptedPolicies();

        Task<IEnumerable<InsurancePolicies>> ShowRejectedPolicies();

        Task<IEnumerable<InsurancePolicies>> GetPoliciesExpiringSoonAsync(DateOnly expirationDate);
    }
}
