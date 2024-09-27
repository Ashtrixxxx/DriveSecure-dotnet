using Backend.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
    }


    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IEmailService _emailService; // Assuming you have an email service

        // Constructor to inject services
        public PasswordController( IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest forgotPasswordRequest)
        {
           

            var token = "kudgdfkweuhfbwejhgfvwejfyhwevbfkujy32fg3i27fgkueybf";

            var resetLink = Url.Action("ResetPassword", "Account", new { token, email = forgotPasswordRequest.Email }, Request.Scheme);

            await _emailService.SendResetPasswordEmail(forgotPasswordRequest.Email, resetLink);

            return Ok("Password reset link has been sent.");
        }
    }
}
