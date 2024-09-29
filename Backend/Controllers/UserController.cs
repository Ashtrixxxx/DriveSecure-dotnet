using Backend.Dto;
using Backend.Models;
using Backend.Repository;
using Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Backend.Controllers
{
    public class EmailRequest
    {
        public string Email { get; set; }
    }

    public class ResetPassword
    {
        public string token { get; set; }
        public string password { get; set; }
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserServices _user;
        public UserController(IUserServices userServices) {
        
            _user = userServices;
        }

        
        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDetails userDetails)
        {
            try
            {
                return Ok(await _user.CreateUser(userDetails));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("unique index"))
                {
                    return BadRequest(new { message = "Username already exists" });
                }

                return StatusCode(500, ex.InnerException.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SendPasswordResetEmail([FromBody] EmailRequest emailRequest)
        {
            if (string.IsNullOrEmpty(emailRequest.Email))
            {
                return BadRequest("Email is required.");
            }

            await _user.SendPasswordResetEmail(emailRequest.Email);
            return Ok();
        }
        [HttpPost]

        public async Task ResetPassword([FromBody] ResetPassword r)
        {
            await _user.ResetPassword(r.token,r.password);
        }





        [Authorize(Roles = "user")]

        [HttpGet()]
        public async Task<UserDetails> UpdateUser(UserDetails userDetails)
        {
            return await _user.UpdateUser(userDetails);
        }

        [Authorize(Roles = "user")]

        [HttpGet("{username}")]
        public async Task<UserDetails> GetUserByUserName(string username)
        {
            return await _user.GetUserByUserName(username);
        }


        [Authorize(Roles = "user")]
        [HttpGet("{UserId}")]
        public async Task<IEnumerable<InsurancePolicies>> GetUserPolicies(int UserId)
        {
            return await _user.UserPolicyDetails(UserId);

        }

        [Authorize(Roles = "user")]

        [HttpPost("{userId}")]
        public async Task OnFormSubmission([FromBody] PaymentCompletionDto paymentCompletionDto, int userId)
        {
            var vehicle = paymentCompletionDto.Vehicle;
            var policyDetails = paymentCompletionDto.PolicyDetails;
            var supportDocuments = paymentCompletionDto.SupportDocuments;

            await _user.OnFormSubmission(userId, vehicle, policyDetails, supportDocuments);
        }

        [HttpPost("{id}")]

        public async Task SimpleTestEmail(int id)
        {
            
           await _user.SimpleTestEmail(id);


        }


    }
}
