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
    public class SupportDocumentControllerTests
    {
        private Mock<ISupportDocumentServices> _mockDocumentServices;
        private SupportDocumentController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockDocumentServices = new Mock<ISupportDocumentServices>();
            _controller = new SupportDocumentController(_mockDocumentServices.Object);
        }

        [Test]
        public async Task GetSupportDocumnetsForPolicy_ShouldReturnDocument_WhenExists()
        {
            // Arrange
            var policyId = 1;
            var document = new SupportDocuments { DocumentId = 1, PolicyID = policyId }; // Mock data

            _mockDocumentServices.Setup(s => s.GetSupportDocumnetsForPolicy(policyId)).ReturnsAsync(document);

            // Act
            var result = await _controller.GetSupportDocumnetsForPolicy(policyId);

            // Assert
            Assert.IsInstanceOf<SupportDocuments>(result);
            Assert.AreEqual(policyId, result.PolicyID);
        }

        [Test]
        public async Task GetAllDocuments_ShouldReturnAllDocuments()
        {
            // Arrange
            var documents = new List<SupportDocuments>
            {
                new SupportDocuments { DocumentId = 1, AddressProof = "Document1" }, // Mock data
                new SupportDocuments { DocumentId = 2, AddressProof = "Document2" }
            };

            _mockDocumentServices.Setup(s => s.GetAllSupportDocument()).ReturnsAsync(documents);

            // Act
            var result = await _controller.GetAllDocuments();

            // Assert
            Assert.IsInstanceOf<IEnumerable<SupportDocuments>>(result);
            Assert.AreEqual(2, ((IEnumerable<SupportDocuments>)result).Count());
        }

        [Test]
        public async Task GetDocuments_ShouldReturnDocument_WhenExists()
        {
            // Arrange
            var id = 1;
            var document = new SupportDocuments {DocumentId = id, AddressProof = "Document1" }; // Mock data

            _mockDocumentServices.Setup(s => s.GetSupportDocumnetsById(id)).ReturnsAsync(document);

            // Act
            var result = await _controller.GetDocuments(id);

            // Assert
            Assert.IsInstanceOf<SupportDocuments>(result);
            Assert.AreEqual(document.  DocumentId, result.DocumentId);
        }

        [Test]
        public async Task CreateSupportDocuments_ShouldReturnCreatedDocument()
        {
            // Arrange
            var supportDocument = new SupportDocuments { AddressProof = "New Document" }; // Mock data

            _mockDocumentServices.Setup(s => s.AddSupportDocument(supportDocument)).ReturnsAsync(supportDocument);

            // Act
            var result = await _controller.CreateSupportDocuments(supportDocument);

            // Assert
            Assert.IsInstanceOf<SupportDocuments>(result);
            Assert.AreEqual(supportDocument.AddressProof, result.AddressProof);
        }

        [Test]
        public async Task UpdateSupportDocuments_ShouldReturnUpdatedDocument()
        {
            // Arrange
            var supportDocument = new SupportDocuments { DocumentId = 1,    AddressProof = "Updated Document" }; // Mock data

            _mockDocumentServices.Setup(s => s.UpdateSupportDocuments(supportDocument)).ReturnsAsync(supportDocument);

            // Act
            var result = await _controller.UpdateSupportDocuments(supportDocument);

            // Assert
            Assert.IsInstanceOf<SupportDocuments>(result);
            Assert.AreEqual(supportDocument.AddressProof, result.AddressProof);
        }

        [Test]
        public async Task DeletePaymentDetails_ShouldCallDeleteMethod_WhenIdExists()
        {
            // Arrange
            var id = 1;

            _mockDocumentServices.Setup(s => s.DeleteSupportDocument(id)).Returns(Task.CompletedTask);

            // Act
            await _controller.DeletePaymentDetails(id);

            // Assert
            _mockDocumentServices.Verify(s => s.DeleteSupportDocument(id), Times.Once);
        }
    }
}

