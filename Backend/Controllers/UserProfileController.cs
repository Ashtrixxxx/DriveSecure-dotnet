using Backend.Dto;
using Backend.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly DriveDbContext _context;
        private static readonly ILog log = LogManager.GetLogger(typeof(UserProfileController));
        public UserProfileController(DriveDbContext context)
        {
            _context = context;
        }



        [HttpGet("{userId}")]
        [Authorize(Roles = "user")]
        public async Task<UserProfile> GetUserProfile(int userId)
        {
            log.Info("Fetching user profile if it exists");

            return await _context.UserProfiles.FirstOrDefaultAsync(u => u.UserID == userId);
        }

        [Authorize(Roles = "user")]
        
        [HttpPost]
        public async Task<UserProfile> CreateUserProfile([FromBody] UserProfile userProfile)
        {

            log.Info("Creating a profile for a user");
            userProfile.IsProfiled = true;  
              _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();
            return userProfile;
        }


        [Authorize(Roles ="user")]
        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileDto userProfileDto)
        {
            //  decode the token to get the user ID
            var userId = userProfileDto.UserId;
            log.Info("Updating a user profile");
            // Retrieve the user based on the user ID from claims
            var user = await _context.UserProfiles.FirstOrDefaultAsync(u => u.UserID == userId);
            if (user == null)
            {
                log.Info("No such user found to update ");
                return NotFound("User not found");
            }
            // Update user details
            user.UserID = userProfileDto.UserId;
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
            log.Info("Profile has been updated succesfully");
            return Ok("Profile updated successfully");
        }
    }
}
