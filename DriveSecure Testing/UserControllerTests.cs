using Backend.Controllers;
using Backend.Models;
using Backend.Repository;
using Backend.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController _controller;
        private Mock<IUserServices> _userServicesMock;

        [SetUp]
        public void Setup()
        {
            _userServicesMock = new Mock<IUserServices>();
            _controller = new UserController(_userServicesMock.Object);
        }

        [Test]
        
        public async Task CreateUser_ShouldReturnOk_WhenUserIsCreated()
        {
            // Arrange
            var userDetails = new UserDetails { UserName = "testuser", Email = "test@example.com" };
            _userServicesMock.Setup(x => x.CreateUser(It.IsAny<UserDetails>())).ReturnsAsync(userDetails);

            // Act
            var result = await _controller.CreateUser(userDetails);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result); // Check if result is OkObjectResult

            var okResult = result as OkObjectResult; // Cast the result to OkObjectResult
            Assert.IsNotNull(okResult); // Ensure it is not null
            Assert.AreEqual(userDetails, okResult.Value); // Check if the returned value is correct
        }


        [Test]
        public async Task SendPasswordResetEmail_ShouldReturnBadRequest_WhenEmailIsNull()
        {
            // Arrange
            var emailRequest = new EmailRequest { Email = null };

            // Act
            var result = await _controller.SendPasswordResetEmail(emailRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task SendPasswordResetEmail_ShouldCallService_WhenEmailIsValid()
        {
            // Arrange
            var emailRequest = new EmailRequest { Email = "test@example.com" };

            // Act
            var result = await _controller.SendPasswordResetEmail(emailRequest);

            // Assert
            _userServicesMock.Verify(x => x.SendPasswordResetEmail(emailRequest.Email), Times.Once);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task ResetPassword_ShouldCallService_WhenCalled()
        {
            // Arrange
            var resetPassword = new ResetPassword { token = "some-token", password = "newPassword123" };

            // Act
            await _controller.ResetPassword(resetPassword);

            // Assert
            _userServicesMock.Verify(x => x.ResetPassword(resetPassword.token, resetPassword.password), Times.Once);
        }

        [Test]
        public async Task GetUserByUserName_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var username = "testuser";
            var userDetails = new UserDetails { UserName = username };
            _userServicesMock.Setup(x => x.GetUserByUserName(username)).ReturnsAsync(userDetails);

            // Act
            var result = await _controller.GetUserByUserName(username);

            // Assert
            Assert.AreEqual(userDetails, result);
        }

       
    }
}
