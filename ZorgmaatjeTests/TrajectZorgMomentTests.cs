using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using ZorgmaatjeWebApi.TrajectZorgMoment.Controllers;
using ZorgmaatjeWebApi.TrajectZorgMoment.Repositories;
using Microsoft.AspNetCore.Authentication;

namespace ZorgmaatjeWebApi.TrajectZorgMoment.Tests
{
    public class TrajectZorgMomentControllerTests
    {
        private readonly Mock<ITrajectZorgMomentRepository> _mockRepository;
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly Mock<ILogger<TrajectZorgMomentController>> _mockLogger;
        private readonly TrajectZorgMomentController _controller;

        public TrajectZorgMomentControllerTests()
        {
            _mockRepository = new Mock<ITrajectZorgMomentRepository>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockLogger = new Mock<ILogger<TrajectZorgMomentController>>();
            _controller = new TrajectZorgMomentController(_mockRepository.Object, _mockAuthenticationService.Object, _mockLogger.Object);
        }


        [Fact]
        public async Task GetTrajectZorgMoment_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            var key = new TrajectZorgMomentKey { TrajectId = 1, ZorgMomentId = 2 };
            _mockRepository.Setup(r => r.GetByIdAsync(key)).ReturnsAsync((TrajectZorgMoment)null);

            // Act
            var result = await _controller.GetTrajectZorgMoment(1, 2);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetTrajectZorgMomenten_ReturnsOk_WithListOfTrajectZorgMomenten()
        {
            // Arrange
            var expected = new List<TrajectZorgMoment>
            {
                new TrajectZorgMoment { trajectId = 1, zorgMomentId = 2, volgorde = 3 },
                new TrajectZorgMoment { trajectId = 4, zorgMomentId = 5, volgorde = 6 }
            };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetTrajectZorgMomenten();

            // Assert
            var okResult = Assert.IsType<List<TrajectZorgMoment>>(result.Value);
            Assert.Equal(expected, okResult);
        }

        [Fact]
        public async Task PostTrajectZorgMoment_ReturnsCreatedAtAction_WhenCreated()
        {
            // Arrange
            var dto = new TrajectZorgMoment { trajectId = 1, zorgMomentId = 2, volgorde = 3 };

            // Act
            var result = await _controller.PostTrajectZorgMoment(dto);

            // Assert
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equivalent(dto, createdAtResult.Value);
        }


        [Fact]
        public async Task PutTrajectZorgMoment_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            var key = new TrajectZorgMomentKey { TrajectId = 1, ZorgMomentId = 2 };
            _mockRepository.Setup(r => r.GetByIdAsync(key)).ReturnsAsync((TrajectZorgMoment)null);

            // Act
            var result = await _controller.PutTrajectZorgMoment(1, 2, new TrajectZorgMoment());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task DeleteTrajectZorgMomentenByPatientId_ReturnsOk_WithAffectedRows()
        {
            // Arrange
            _mockRepository.Setup(r => r.DeleteTrajectZorgMomentenByPatientIdAsync("patient1")).ReturnsAsync(2);

            // Act
            var result = await _controller.DeleteTrajectZorgMomentenByPatientId("patient1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(2, okResult.Value);
        }

        [Fact]
        public async Task DeleteTrajectZorgMomentenByPatientId_ReturnsNotFound_WhenNoRowsAffected()
        {
            // Arrange
            _mockRepository.Setup(r => r.DeleteTrajectZorgMomentenByPatientIdAsync("patient1")).ReturnsAsync(0);

            // Act
            var result = await _controller.DeleteTrajectZorgMomentenByPatientId("patient1");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}