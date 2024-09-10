using Backend.Models;
using Backend.Repository;
using Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserServices _user;
        public UserController(IUserServices userServices) {
        
            _user = userServices;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<UserDetails> CreateUser(UserDetails userDetails)
        {
            return await _user.CreateUser(userDetails);
        }


        [Authorize(Roles = "user")]

        [HttpGet]
        public async Task<UserDetails> UpdateUser(UserDetails userDetails)
        {
            return await _user.UpdateUser(userDetails);
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IEnumerable<InsurancePolicies>> GetUserPolicies(int UserId)
        {
            return await _user.UserPolicyDetails(UserId);

        }

        [Authorize(Roles = "user")]

        [HttpPost]
        public async Task OnPaymentCompletion([FromBody] PaymentCompletionDto paymentCompletionDto)
        {
            var vehicle = paymentCompletionDto.Vehicle;
            var policyDetails = paymentCompletionDto.PolicyDetails;
            var paymentDetails = paymentCompletionDto.PaymentDetails;
            var supportDocuments = paymentCompletionDto.SupportDocuments;

            await _user.OnPaymentCompletion(vehicle, policyDetails, paymentDetails, supportDocuments);
        }


    }
}
