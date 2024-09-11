using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Authorization;
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


        [Authorize(Policy = "AdminAndUser")]

        [HttpGet]
        public async Task<IEnumerable<InsurancePolicies>> GetAllPolicies()
        {
            return await _policyServices.GetAllPolicies();
        }


        [Authorize(Policy = "AdminAndUser")]

        [HttpGet]
        public async Task<InsurancePolicies> GetPolicyStatus(int PolicyId)
        {
            return await _policyServices.GetPolicyStatus(PolicyId);
        }

        [Authorize(Roles = "user")]
        [HttpPost("{PolicyId}")]
        public async Task<InsurancePolicies> PolicyAccepted(int PolicyId)
        {
            return  await _policyServices.PolicyAccepted(PolicyId);
        }

        [Authorize(Roles = "user")]
        [HttpPost("{PolicyId}")]
        public async Task<InsurancePolicies> PolicyRejected(int PolicyId)
        {
            return await _policyServices.PolicyRejected(PolicyId);
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IEnumerable<InsurancePolicies>> ShowAcceptedPolicies()
        {
            return await _policyServices.ShowAcceptedPolicies();
        }


        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IEnumerable<InsurancePolicies>> ShowRejectedPolicies()
        {
            return await _policyServices.ShowRejectedPolicies();
        }

    }
}
