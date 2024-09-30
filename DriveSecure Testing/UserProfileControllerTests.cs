using Backend.Controllers;
using Backend.Dto;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Tests.Controllers
{
    [TestFixture]
    public class UserProfileControllerTests
    {
        private UserProfileController _controller;
        private DriveDbContext _context;

        [SetUp]
        public void SetUp()
        {
            // Use in-memory database for testing
            var options = new DbContextOptionsBuilder<DriveDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new DriveDbContext(options);
            _controller = new UserProfileController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose the context after each test
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        

        [Test]
        public async Task CreateUserProfile_ShouldAddUserProfile_WhenCalled()
        {
            // Arrange
            var userProfile = new UserProfile
            {
                UserID = 1,
                ProfileUrl = "http://example.com/profile.jpg",
                FirstName = "Test",
                LastName = "User",
                DOB = DateOnly.Parse("1990-01-01"),
                Gender = "Male",
                Phone = "1234567890",
                Occupation = "Developer",
                StreetAddr = "123 Main St",
                Country = "USA",
                Zipcode = "12345",
                City = "New York",
                IsProfiled = true
            };

            // Set up controller context with claims
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userProfile.UserID.ToString())
                    }))
                }
            };

            // Act
            var result = await _controller.CreateUserProfile(userProfile);

            // Assert
            Assert.IsInstanceOf<UserProfile>(result);
            var createdUserProfile = result as UserProfile;
            Assert.AreEqual(userProfile.ProfileUrl, createdUserProfile.ProfileUrl);

            // Verify that the user profile was added to the context
            var addedProfile = await _context.UserProfiles.FindAsync(userProfile.UserID);
            Assert.IsNotNull(addedProfile);
            Assert.AreEqual(userProfile.ProfileUrl, addedProfile.ProfileUrl);
        }

        [Test]
        public async Task UpdateUserProfile_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userProfileDto = new UserProfileDto();
            var userId = "1"; // Mocked user ID from claims
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userId)
                    }))
                }
            };

            // Act
            var result = await _controller.UpdateUserProfile(userProfileDto);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual("User not found", notFoundResult.Value);
        }
    }
}


