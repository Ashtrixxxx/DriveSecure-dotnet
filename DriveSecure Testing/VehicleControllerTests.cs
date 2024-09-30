using Backend.Controllers;
using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DriveSecureTesting
{
    [TestFixture]
    public class VehicleControllerTests
    {
        private Mock<IVehicleServices> _mockVehicleServices;
        private VehicleController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockVehicleServices = new Mock<IVehicleServices>();
            _controller = new VehicleController(_mockVehicleServices.Object);
        }

        [Test]
        public async Task GetAllVehicles_ShouldReturnVehicles_WhenUserExists()
        {
            // Arrange
            var vid = 1;
            var vehicles = new List<VehicleDetails>
            {
                new VehicleDetails {VehicleId= vid, VehicleType = "Car" } // Mock data
            };

            _mockVehicleServices.Setup(s => s.GetAllVehiclesAsync(vid)).ReturnsAsync(vehicles);

            // Act
            var result = await _controller.GetAllVehicles(vid);

            // Assert
            Assert.IsInstanceOf<IEnumerable<VehicleDetails>>(result);
            Assert.AreEqual(1, ((IEnumerable<VehicleDetails>)result).Count());
        }

        [Test]
        public async Task GetVehicleForPolicy_ShouldReturnVehicle_WhenPolicyExists()
        {
            // Arrange
            var policyID = 1;
            var vehicle = new VehicleDetails { VehicleId = 1, VehicleType = "Truck" }; // Mock data

            _mockVehicleServices.Setup(s => s.GetVehicleForPolicy(policyID)).ReturnsAsync(vehicle);

            // Act
            var result = await _controller.GetVehicleForPolicy(policyID);

            // Assert
            Assert.IsInstanceOf<VehicleDetails>(result);
            Assert.AreEqual(vehicle.VehicleId, result.VehicleId);
        }

        [Test]
        public async Task GetVehicleById_ShouldReturnVehicle_WhenVidExists()
        {
            // Arrange
            var vid = 1;
            var vehicle = new VehicleDetails { VehicleId = vid, VehicleType = "Car" }; // Mock data

            _mockVehicleServices.Setup(s => s.GetVehiclesAsync(vid)).ReturnsAsync(vehicle);

            // Act
            var result = await _controller.GetVehicleById(vid);

            // Assert
            Assert.IsInstanceOf<VehicleDetails>(result);
            Assert.AreEqual(vehicle.VehicleId, result.VehicleId);
        }

        [Test]
        public async Task UpdateVehicle_ShouldReturnUpdatedVehicle_WhenSuccess()
        {
            // Arrange
            var vehicleDetails = new VehicleDetails { VehicleId = 1, VehicleType = "Motorcycle" }; // Mock data

            _mockVehicleServices.Setup(s => s.UpdateVehicle(vehicleDetails)).ReturnsAsync(vehicleDetails);

            // Act
            var result = await _controller.UpdateVehicle(vehicleDetails);

            // Assert
            Assert.IsInstanceOf<VehicleDetails>(result);
            Assert.AreEqual(vehicleDetails.VehicleId, result.VehicleId);
        }
    }
}
