using Backend.Models;
using Backend.Repository;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PolicyController));
        private readonly IPolicyServices _policyServices;

        public PolicyController(IPolicyServices policyServices)
        {

            _policyServices = policyServices;

        }


        [Authorize(Policy = "AdminAndUser")]

        [HttpGet("{UserId}")]
        public async Task<IEnumerable<InsurancePolicies>> GetAllPolicies(int UserId)
        {
            log.Info("Fetching all the policies for the user");
            return await _policyServices.GetAllPolicies(UserId);
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
       public async Task<IEnumerable<InsurancePolicies>> GetAllPoliciesForAdmin()
        {
            log.Info("Fetching policies for admin");
            return await _policyServices.GetAllPoliciesForAdmin();
        }




        [Authorize(Policy = "AdminAndUser")]

        [HttpGet]
        public async Task<InsurancePolicies> GetPolicyStatus(int PolicyId)
        {
            return await _policyServices.GetPolicyStatus(PolicyId);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{PolicyId}")]
        public async Task<InsurancePolicies> PolicyAccepted(int PolicyId)
        {
            log.Info("Policy has been accepted");
            return  await _policyServices.PolicyAccepted(PolicyId);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{PolicyId}")]
        public async Task<InsurancePolicies> PolicyRejected(int PolicyId)
        {
            log.Info("Policy has been rejected");

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
