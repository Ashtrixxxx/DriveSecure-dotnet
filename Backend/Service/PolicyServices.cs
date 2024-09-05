using Backend.Models;
using Backend.Repository;

namespace Backend.Service
{
    public class PolicyServices : IPolicyServices
    {
       public async Task CreatePolicy(InsurancePolicies insurancePolicies)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InsurancePolicies>> GetAllPolicies()
        {
            throw new NotImplementedException();
        }

      public async  Task<InsurancePolicies> GetPolicyDetails(int PolicyId)
        {
            throw new NotImplementedException();
        }
    }
}
