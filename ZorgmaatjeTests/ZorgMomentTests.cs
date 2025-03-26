using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZorgmaatjeWebApi.ZorgMoment.Controllers;
using ZorgmaatjeWebApi.ZorgMoment.Repositories;

namespace ZorgmaatjeWebApi.ZorgMoment.Tests
{
    public class ZorgMomentControllerTests
    {
        private readonly Mock<IZorgMomentRepository> _mockRepository;
        private readonly ZorgMomentController _controller;

        public ZorgMomentControllerTests()
        {
            _mockRepository = new Mock<IZorgMomentRepository>();
            _controller = new ZorgMomentController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetZorgMoment_ReturnsOk_WhenFound()
        {
            // Arrange
            var expected = new ZorgMoment { id = 1, naam = "Test", patientId = "patient1" };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetZorgMoment(1);

            // Assert
            var okResult = Assert.IsType<ZorgMoment>(result.Value);
            Assert.Equal(expected, okResult);
        }

        [Fact]
        public async Task GetZorgMoment_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((ZorgMoment)null);

            // Act
            var result = await _controller.GetZorgMoment(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetZorgMomentByNameAndPatientId_ReturnsOk_WhenFound()
        {
            // Arrange
            var expected = new ZorgMoment { id = 1, naam = "Test", patientId = "patient1" };
            _mockRepository.Setup(r => r.GetZorgMomentByNameAndPatientIdAsync("Test", "patient1")).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetZorgMomentByNameAndPatientId("Test", "patient1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task GetZorgMomentByNameAndPatientId_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetZorgMomentByNameAndPatientIdAsync("Test", "patient1")).ReturnsAsync((ZorgMoment)null);

            // Act
            var result = await _controller.GetZorgMomentByNameAndPatientId("Test", "patient1");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetZorgMomentenByPatientIdSortedByVolgorde_ReturnsOk_WithListOfZorgMomenten()
        {
            // Arrange
            var expected = new List<dynamic> { new { id = 1, naam = "Test" } };
            _mockRepository.Setup(r => r.GetZorgMomentenByPatientIdSortedByVolgordeAsync("patient1")).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetZorgMomentenByPatientIdSortedByVolgorde("patient1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task GetZorgMomenten_ReturnsOk_WithListOfZorgMomenten()
        {
            // Arrange
            var expected = new List<ZorgMoment> { new ZorgMoment { id = 1, naam = "Test", patientId = "patient1" } };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetZorgMomenten();

            // Assert
            var okResult = Assert.IsType<List<ZorgMoment>>(result.Value);
            Assert.Equal(expected, okResult);
        }

        [Fact]
        public async Task PostZorgMoment_ReturnsCreatedAtAction_WhenCreated()
        {
            // Arrange
            var zorgMoment = new ZorgMoment { id = 1, naam = "Test", patientId = "patient1" };

            // Act
            var result = await _controller.PostZorgMoment(zorgMoment);

            // Assert
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(zorgMoment, createdAtResult.Value);
        }

        [Fact]
        public async Task PutZorgMoment_ReturnsCreatedAtAction_WhenUpdated()
        {
            // Arrange
            var zorgMoment = new ZorgMoment { id = 1, naam = "Updated", patientId = "patient1" };

            // Act
            var result = await _controller.PutZorgMoment(1, zorgMoment);

            // Assert
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(zorgMoment, createdAtResult.Value);
        }

        [Fact]
        public async Task PutZorgMoment_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var zorgMoment = new ZorgMoment { id = 2, naam = "Test", patientId = "patient1" };

            // Act
            var result = await _controller.PutZorgMoment(1, zorgMoment);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteZorgMoment_ReturnsOk_WhenDeleted()
        {
            // Act
            var result = await _controller.DeleteZorgMoment(1);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockRepository.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteZorgMomentenByPatientId_ReturnsOk_WithAffectedRows()
        {
            // Arrange
            _mockRepository.Setup(r => r.DeleteZorgMomentenByPatientIdAsync("patient1")).ReturnsAsync(2);

            // Act
            var result = await _controller.DeleteZorgMomentenByPatientId("patient1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(2, okResult.Value);
        }

        [Fact]
        public async Task DeleteZorgMomentenByPatientId_ReturnsNotFound_WhenNoRowsAffected()
        {
            // Arrange
            _mockRepository.Setup(r => r.DeleteZorgMomentenByPatientIdAsync("patient1")).ReturnsAsync(0);

            // Act
            var result = await _controller.DeleteZorgMomentenByPatientId("patient1");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}