using Backend.Models;

namespace Backend.Repository
{
    public interface IPolicyServices
    {

        Task<InsurancePolicies> CreatePolicy(InsurancePolicies insurancePolicies);

        Task<IEnumerable<InsurancePolicies>> GetAllPolicies();

        Task <InsurancePolicies> GetPolicyDetails(int PolicyId);



    }
}
