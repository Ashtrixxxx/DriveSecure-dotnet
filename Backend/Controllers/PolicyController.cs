using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {

        private readonly IPolicyServices _policyServices;

        public PolicyController(IPolicyServices policyServices)
        {

            _policyServices = policyServices;

        }

        [HttpGet]
        public async Task<IEnumerable<InsurancePolicies>> GetAllPolicies()
        {
            return await _policyServices.GetAllPolicies();
        }

        [HttpGet]
        public async Task<InsurancePolicies> GetPolicyStatus(int PolicyId)
        {
            return await _policyServices.GetPolicyStatus(PolicyId);
        }
    }
}
