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
    public class TempFormControllerTests
    {
        private Mock<ITempFormDataServices> _mockTempFormServices;
        private TempFormController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockTempFormServices = new Mock<ITempFormDataServices>();
            _controller = new TempFormController(_mockTempFormServices.Object);
        }

        [Test]
        public async Task GetAllTempForm_ShouldReturnAllTempForms()
        {
            // Arrange
            var tempForms = new List<TempFormData>
            {
                new TempFormData { FormId = 1, FormData = "Form1" }, // Mock data
                new TempFormData { FormId = 2, FormData = "Form2" }
            };

            _mockTempFormServices.Setup(s => s.GetAllTempFormData()).ReturnsAsync(tempForms);

            // Act
            var result = await _controller.GetAllTempForm();

            // Assert
            Assert.IsInstanceOf<IEnumerable<TempFormData>>(result);
            Assert.AreEqual(2, ((IEnumerable<TempFormData>)result).Count());
        }

        [Test]
        public async Task GetTempForm_ShouldReturnTempForm_WhenExists()
        {
            // Arrange
            var id = 1;
            var tempForm = new TempFormData { FormId = id, FormData = "Form1" }; // Mock data

            _mockTempFormServices.Setup(s => s.GetTempFormDataById(id)).ReturnsAsync(tempForm);

            // Act
            var result = await _controller.GetTempForm(id);

            // Assert
            Assert.IsInstanceOf<TempFormData>(result);
            Assert.AreEqual(tempForm.FormId, result.FormId);
        }

        [Test]
        public async Task CreateTempForm_ShouldReturnCreatedTempForm()
        {
            // Arrange
            var tempForm = new TempFormData { FormData = "New Form" }; // Mock data

            _mockTempFormServices.Setup(s => s.AddTempFormData(tempForm)).ReturnsAsync(tempForm);

            // Act
            var result = await _controller.CreateTempForm(tempForm);

            // Assert
            Assert.IsInstanceOf<TempFormData>(result);
            Assert.AreEqual(tempForm.FormData, result.FormData);
        }

        [Test]
        public async Task UpdateTempForm_ShouldReturnUpdatedTempForm()
        {
            // Arrange
            var tempForm = new TempFormData {   FormId = 1, FormData  = "Updated Form" }; // Mock data

            _mockTempFormServices.Setup(s => s.UpdateTempFormData(tempForm)).ReturnsAsync(tempForm);

            // Act
            var result = await _controller.UpdateTempForm(tempForm);

            // Assert
            Assert.IsInstanceOf<TempFormData>(result);
            Assert.AreEqual(tempForm.FormData, result.FormData);
        }

        [Test]
        public async Task DeleteTempForm_ShouldCallDeleteMethod_WhenIdExists()
        {
            // Arrange
            var id = 1;

            _mockTempFormServices.Setup(s => s.DeleteTempFormData(id)).Returns(Task.CompletedTask);

            // Act
            await _controller.DeleteTempForm(id);

            // Assert
            _mockTempFormServices.Verify(s => s.DeleteTempFormData(id), Times.Once);
        }
    }
}
