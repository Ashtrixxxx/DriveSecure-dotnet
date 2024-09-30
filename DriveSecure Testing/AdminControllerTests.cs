using Backend.Controllers;
using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourAppNamespace.Tests
{
    [TestFixture]
    public class AdminControllerTests
    {
        private Mock<IAdminService> _adminServiceMock;
        private AdminController _controller;

        [SetUp]
        public void SetUp()
        {
            _adminServiceMock = new Mock<IAdminService>();
            _controller = new AdminController(_adminServiceMock.Object);
        }

        [Test]
        public async Task CreateAdmin_ShouldCallAddAdmin()
        {
            // Arrange
            var admin = new AdminDetails { /* Initialize properties */ };

            // Act
            await _controller.CreateAdmin(admin);

            // Assert
            _adminServiceMock.Verify(s => s.AddAdmin(admin), Times.Once);
        }

        [Test]
        public async Task GetAllAdmins_ShouldReturnListOfAdmins()
        {
            // Arrange
            var admins = new List<AdminDetails>
            {
                new AdminDetails { /* Initialize properties */ },
                new AdminDetails { /* Initialize properties */ }
            };

            _adminServiceMock.Setup(s => s.GetAllAdmins()).ReturnsAsync(admins);

            // Act
            var result = await _controller.GetAllAdmins();

            // Assert
            Assert.IsInstanceOf<IEnumerable<AdminDetails>>(result);
            Assert.AreEqual(2, ((IEnumerable<AdminDetails>)result).Count());
        }

        [Test]
        public async Task GetAdmin_ShouldReturnAdmin()
        {
            // Arrange
            int adminId = 1;
            var admin = new AdminDetails { /* Initialize properties */ };

            _adminServiceMock.Setup(s => s.GetAdminById(adminId)).ReturnsAsync(admin);

            // Act
            var result = await _controller.GetAdmin(adminId);

            // Assert
            Assert.IsInstanceOf<AdminDetails>(result);
            Assert.AreEqual(admin, result);
        }

        [Test]
        public async Task DeleteAdmin_ShouldCallDeleteAdmin()
        {
            // Arrange
            int adminId = 1;

            // Act
            await _controller.DeleteAdmin(adminId);

            // Assert
            _adminServiceMock.Verify(s => s.DeleteAdmin(adminId), Times.Once);
        }

        [Test]
        public async Task UpdateAdmin_ShouldCallUpdateAdmin()
        {
            // Arrange
            var admin = new AdminDetails { /* Initialize properties */ };

            // Act
            await _controller.UpdateAdmin(admin);

            // Assert
            _adminServiceMock.Verify(s => s.UpdateAdmin(admin), Times.Once);
        }
    }
}

