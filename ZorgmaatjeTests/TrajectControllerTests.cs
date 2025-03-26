using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ZorgmaatjeWebApi.Traject.Controllers;
using ZorgmaatjeWebApi.Traject.Repositories;

namespace ZorgmaatjeWebApi.Traject.Tests
{
    public class TrajectControllerTests
    {
        private readonly Mock<ITrajectRepository> _mockTrajectRepository;
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly Mock<ILogger<TrajectController>> _mockLogger;
        private readonly TrajectController _trajectController;

        public TrajectControllerTests()
        {
            _mockTrajectRepository = new Mock<ITrajectRepository>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockLogger = new Mock<ILogger<TrajectController>>();
            _trajectController = new TrajectController(_mockTrajectRepository.Object, _mockAuthenticationService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetTraject_ReturnsOkWithTraject_WhenTrajectExists()
        {
            // Arrange
            string trajectNaam = "TestTraject";
            var expectedTraject = new Traject { naam = trajectNaam, id = 1 };
            _mockTrajectRepository.Setup(x => x.GetTrajectByNaamAsync(trajectNaam)).ReturnsAsync(expectedTraject);

            // Act
            var result = await _trajectController.GetTraject(trajectNaam);

            // Assert
            var trajectResult = Assert.IsType<Traject>(result.Value);
            Assert.Equal(expectedTraject, trajectResult);
        }

        [Fact]
        public async Task GetTraject_ReturnsNotFound_WhenTrajectDoesNotExist()
        {
            // Arrange
            string trajectNaam = "NonExistentTraject";
            _mockTrajectRepository.Setup(x => x.GetTrajectByNaamAsync(trajectNaam)).ReturnsAsync((Traject)null);

            // Act
            var result = await _trajectController.GetTraject(trajectNaam);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetTrajects_ReturnsOkWithListOfTrajects()
        {
            // Arrange
            var expectedTrajects = new List<Traject>
            {
                new Traject { naam = "Traject1", id = 1 },
                new Traject { naam = "Traject2", id = 2 }
            };
            _mockTrajectRepository.Setup(x => x.GetAllTrajectsAsync()).ReturnsAsync(expectedTrajects);

            // Act
            var result = await _trajectController.GetTrajects();

            // Assert
            var trajectsResult = Assert.IsType<List<Traject>>(result.Value);
            Assert.Equal(expectedTrajects.Count, trajectsResult.Count);
        }

        [Fact]
        public async Task CreateTraject_ReturnsCreatedAtAction_WhenTrajectIsCreated()
        {
            // Arrange
            var newTraject = new Traject { naam = "NewTraject" };

            // Act
            var result = await _trajectController.CreateTraject(newTraject);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(newTraject, createdAtActionResult.Value);
        }

        [Fact]
        public async Task CreateTraject_ReturnsConflict_WhenTrajectAlreadyExists()
        {
            // Arrange
            var newTraject = new Traject { naam = "ExistingTraject" };
            _mockTrajectRepository.Setup(x => x.AddTrajectAsync(It.IsAny<Traject>())).ThrowsAsync(new Exception("Traject already exists"));

            // Act
            var result = await _trajectController.CreateTraject(newTraject);

            // Assert
            Assert.IsType<ConflictObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateTraject_ReturnsCreatedAtAction_WhenTrajectIsUpdated()
        {
            // Arrange
            string trajectNaam = "TestTraject";
            var existingTraject = new Traject { naam = trajectNaam, id = 1 };
            var updatedTraject = new Traject { naam = "UpdatedTraject", id = 1 };
            _mockTrajectRepository.Setup(x => x.GetTrajectByNaamAsync(trajectNaam)).ReturnsAsync(existingTraject);

            // Act
            var result = await _trajectController.UpdateTraject(trajectNaam, updatedTraject);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(updatedTraject, createdAtActionResult.Value);
        }

        [Fact]
        public async Task UpdateTraject_ReturnsNotFound_WhenTrajectDoesNotExist()
        {
            // Arrange
            string trajectNaam = "NonExistentTraject";
            var updatedTraject = new Traject { naam = "UpdatedTraject", id = 1 };
            _mockTrajectRepository.Setup(x => x.GetTrajectByNaamAsync(trajectNaam)).ReturnsAsync((Traject)null);

            // Act
            var result = await _trajectController.UpdateTraject(trajectNaam, updatedTraject);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteTraject_ReturnsOk_WhenTrajectIsDeleted()
        {
            // Arrange
            string trajectNaam = "TestTraject";
            var existingTraject = new Traject { naam = trajectNaam, id = 1 };
            _mockTrajectRepository.Setup(x => x.GetTrajectByNaamAsync(trajectNaam)).ReturnsAsync(existingTraject);

            // Act
            var result = await _trajectController.DeleteTraject(trajectNaam);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(trajectNaam, okResult.Value);
        }

        [Fact]
        public async Task DeleteTraject_ReturnsNotFound_WhenTrajectDoesNotExist()
        {
            // Arrange
            string trajectNaam = "NonExistentTraject";
            _mockTrajectRepository.Setup(x => x.GetTrajectByNaamAsync(trajectNaam)).ReturnsAsync((Traject)null);

            // Act
            var result = await _trajectController.DeleteTraject(trajectNaam);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}