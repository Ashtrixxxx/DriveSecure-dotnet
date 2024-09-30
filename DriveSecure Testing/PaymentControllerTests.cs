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
    public class PaymentControllerTests
    {
        private PaymentController _controller;
        private Mock<IPaymentServices> _paymentServicesMock;
        private Mock<IPolicyServices> _policyServicesMock;

        [SetUp]
        public void SetUp()
        {
            _paymentServicesMock = new Mock<IPaymentServices>();
            _policyServicesMock = new Mock<IPolicyServices>();
            _controller = new PaymentController(_paymentServicesMock.Object, _policyServicesMock.Object);
        }

        [Test]
        public async Task GetAllPayment_ShouldReturnPaymentDetails()
        {
            // Arrange
            var paymentDetailsList = new List<PaymentDetails>
            {
                new PaymentDetails { /* Initialize with properties */ },
                new PaymentDetails { /* Initialize with properties */ }
            };

            _paymentServicesMock.Setup(s => s.GetAllPaymentDetails()).ReturnsAsync(paymentDetailsList);

            // Act
            var result = await _controller.GetAllPayment();

            // Assert
            Assert.IsInstanceOf<IEnumerable<PaymentDetails>>(result);
            Assert.AreEqual(paymentDetailsList.Count, ((IEnumerable<PaymentDetails>)result).Count());
        }

        [Test]
        public async Task GetPayments_ShouldReturnPaymentDetail_WhenExists()
        {
            // Arrange
            var paymentId = 1;
            var paymentDetail = new PaymentDetails { /* Initialize with properties */ };
            _paymentServicesMock.Setup(s => s.GetPaymentDetailsById(paymentId)).ReturnsAsync(paymentDetail);

            // Act
            var result = await _controller.GetPayments(paymentId);

            // Assert
            Assert.IsInstanceOf<PaymentDetails>(result);
            Assert.AreEqual(paymentDetail, result);
        }

        [Test]
        public async Task CreatePaymentDetails_ShouldReturnCreatedPaymentDetail()
        {
            // Arrange
            var paymentDetail = new PaymentDetails { /* Initialize with properties */ };
            var dummyInsurancePolicy = new InsurancePolicies { /* Initialize properties as needed */ };

            _policyServicesMock.Setup(s => s.PolicyPaid(paymentDetail.PolicyID)).ReturnsAsync(dummyInsurancePolicy);
            _paymentServicesMock.Setup(s => s.AddPaymentDetails(paymentDetail)).ReturnsAsync(paymentDetail);

            // Act
            var result = await _controller.CreatePaymentDetails(paymentDetail);

            // Assert
            Assert.IsInstanceOf<PaymentDetails>(result);
            Assert.AreEqual(paymentDetail, result);
        }

        [Test]
        public async Task UpdatePayment_ShouldReturnUpdatedPaymentDetail()
        {
            // Arrange
            var paymentDetail = new PaymentDetails { /* Initialize with properties */ };
            _paymentServicesMock.Setup(s => s.UpdatePaymentDetails(paymentDetail)).ReturnsAsync(paymentDetail);

            // Act
            var result = await _controller.UpdatePayment(paymentDetail);

            // Assert
            Assert.IsInstanceOf<PaymentDetails>(result);
            Assert.AreEqual(paymentDetail, result);
        }

        [Test]
        public async Task DeletePaymentDetails_ShouldCallServiceToDelete()
        {
            // Arrange
            var paymentId = 1;

            // Act
            await _controller.DeletePaymentDetails(paymentId);

            // Assert
            _paymentServicesMock.Verify(s => s.DeletePaymentDetails(paymentId), Times.Once);
        }
    }
}
