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
    public class PolicyControllerTests
    {
        private Mock<IPolicyServices> _mockPolicyServices;
        private PolicyController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockPolicyServices = new Mock<IPolicyServices>();
            _controller = new PolicyController(_mockPolicyServices.Object);
        }

        [Test]
        public async Task GetAllPolicies_ShouldReturnPolicies_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var policies = new List<InsurancePolicies>
            {
                new InsurancePolicies { PolicyID = 1, UserID = userId }, // Mock data
                new InsurancePolicies { PolicyID = 2, UserID = userId }
            };

            _mockPolicyServices.Setup(s => s.GetAllPolicies(userId)).ReturnsAsync(policies);

            // Act
            var result = await _controller.GetAllPolicies(userId);

            // Assert
            Assert.IsInstanceOf<IEnumerable<InsurancePolicies>>(result);
            Assert.AreEqual(2, ((IEnumerable<InsurancePolicies>)result).Count());
        }

        [Test]
        public async Task GetAllPoliciesForAdmin_ShouldReturnAllPolicies_WhenAdmin()
        {
            // Arrange
            var policies = new List<InsurancePolicies>
            {
                new InsurancePolicies { PolicyID = 1 }, // Mock data
                new InsurancePolicies { PolicyID = 2 }
            };

            _mockPolicyServices.Setup(s => s.GetAllPoliciesForAdmin()).ReturnsAsync(policies);

            // Act
            var result = await _controller.GetAllPoliciesForAdmin();

            // Assert
            Assert.IsInstanceOf<IEnumerable<InsurancePolicies>>(result);
            Assert.AreEqual(2, ((IEnumerable<InsurancePolicies>)result).Count());
        }

        [Test]
        public async Task GetPolicyStatus_ShouldReturnPolicy_WhenExists()
        {
            // Arrange
            var policyId = 1;
            var policy = new InsurancePolicies { PolicyID = policyId }; // Mock data

            _mockPolicyServices.Setup(s => s.GetPolicyStatus(policyId)).ReturnsAsync(policy);

            // Act
            var result = await _controller.GetPolicyStatus(policyId);

            // Assert
            Assert.IsInstanceOf<InsurancePolicies>(result);
            Assert.AreEqual(policyId, result.PolicyID);
        }

        [Test]
        public async Task PolicyAccepted_ShouldReturnAcceptedPolicy_WhenAdmin()
        {
            // Arrange
            var policyId = 1;
            var policy = new InsurancePolicies { PolicyID = policyId, Status = 1 }; // Mock data

            _mockPolicyServices.Setup(s => s.PolicyAccepted(policyId)).ReturnsAsync(policy);

            // Act
            var result = await _controller.PolicyAccepted(policyId);

            // Assert
            Assert.IsInstanceOf<InsurancePolicies>(result);
            Assert.AreEqual(1, result.Status);
        }

        [Test]
        public async Task PolicyRejected_ShouldReturnRejectedPolicy_WhenAdmin()
        {
            // Arrange
            var policyId = 1;
            var policy = new InsurancePolicies { PolicyID = policyId, Status = 2 }; // Mock data

            _mockPolicyServices.Setup(s => s.PolicyRejected(policyId)).ReturnsAsync(policy);

            // Act
            var result = await _controller.PolicyRejected(policyId);

            // Assert
            Assert.IsInstanceOf<InsurancePolicies>(result);
            Assert.AreEqual(2, result.Status);
        }

        [Test]
        public async Task ShowAcceptedPolicies_ShouldReturnAcceptedPolicies_WhenUser()
        {
            // Arrange
            var policies = new List<InsurancePolicies>
            {
                new InsurancePolicies { PolicyID = 1, Status = 1 }, // Mock data
                new InsurancePolicies { PolicyID = 2, Status = 1     }
            };

            _mockPolicyServices.Setup(s => s.ShowAcceptedPolicies()).ReturnsAsync(policies);

            // Act
            var result = await _controller.ShowAcceptedPolicies();

            // Assert
            Assert.IsInstanceOf<IEnumerable<InsurancePolicies>>(result);
            Assert.AreEqual(2, ((IEnumerable<InsurancePolicies>)result).Count());
        }

        [Test]
        public async Task ShowRejectedPolicies_ShouldReturnRejectedPolicies_WhenUser()
        {
            // Arrange
            var policies = new List<InsurancePolicies>
            {
                new InsurancePolicies { PolicyID = 1, Status = 2 }, // Mock data
                new InsurancePolicies { PolicyID = 2, Status = 2 }
            };

            _mockPolicyServices.Setup(s => s.ShowRejectedPolicies()).ReturnsAsync(policies);

            // Act
            var result = await _controller.ShowRejectedPolicies();

            // Assert
            Assert.IsInstanceOf<IEnumerable<InsurancePolicies>>(result);
            Assert.AreEqual(2, ((IEnumerable<InsurancePolicies>)result).Count());
        }
    }
}
