using Backend.Dto;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly DriveDbContext _context;

        public UserProfileController(DriveDbContext context)
        {
            _context = context;
        }

        // GET: api/UserProfile
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileDto userProfileDto)
        {
            //  decode the token to get the user ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            // Retrieve the user based on the user ID from claims
            var user = await _context.UserProfiles.FirstOrDefaultAsync(u => u.UserID.ToString() == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            // Update user details
            user.ProfileUrl = userProfileDto.ProfileUrl;
            user.FirstName = userProfileDto.FirstName;
            user.LastName = userProfileDto.LastName;
            user.DOB = userProfileDto.DOB;
            user.Gender = userProfileDto.Gender;
            user.Phone = userProfileDto.Phone;
            user.Occupation = userProfileDto.Occupation;
            user.StreetAddr = userProfileDto.StreetAddr;
            user.Country = userProfileDto.Country;
            user.Zipcode = userProfileDto.Zipcode;
            user.City = userProfileDto.City;
            user.IsProfiled = true; // Mark as profiled

            await _context.SaveChangesAsync();

            return Ok("Profile updated successfully");
        }
    }
}
