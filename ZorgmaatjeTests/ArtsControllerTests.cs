using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ZorgmaatjeWebApi.Arts.Controller;
using ZorgmaatjeWebApi.Arts.Repositories;
using Microsoft.AspNetCore.Authentication;

namespace ZorgmaatjeWebApi.Arts.Tests
{
    public class ArtsControllerTests
    {
        private readonly Mock<IArtsRepository> _mockArtsRepository;
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly Mock<ILogger<ArtsController>> _mockLogger;
        private readonly ArtsController _artsController;

        public ArtsControllerTests()
        {
            _mockArtsRepository = new Mock<IArtsRepository>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockLogger = new Mock<ILogger<ArtsController>>();
            _artsController = new ArtsController(_mockArtsRepository.Object, _mockAuthenticationService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetArts_ReturnsOkWithArts_WhenArtsExists()
        {
            // Arrange
            string artsNaam = "TestArts";
            var expectedArts = new Arts { naam = artsNaam, id = "123" };
            _mockArtsRepository.Setup(x => x.GetArtsByNaamAsync(artsNaam)).ReturnsAsync(expectedArts);

            // Act
            var result = await _artsController.GetArts(artsNaam);

            // Assert
            var artsResult = Assert.IsType<Arts>(result.Value);
            Assert.Equal(expectedArts, artsResult);
        }

        [Fact]
        public async Task GetArts_ReturnsNotFound_WhenArtsDoesNotExist()
        {
            // Arrange
            string artsNaam = "NonExistentArts";
            _mockArtsRepository.Setup(x => x.GetArtsByNaamAsync(artsNaam)).ReturnsAsync((Arts)null);

            // Act
            var result = await _artsController.GetArts(artsNaam);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetArtsen_ReturnsOkWithListOfArts()
        {
            // Arrange
            var expectedArtsen = new List<Arts>
            {
                new Arts { naam = "Arts1", id = "1" },
                new Arts { naam = "Arts2", id = "2" }
            };
            _mockArtsRepository.Setup(x => x.GetAllArtsAsync()).ReturnsAsync(expectedArtsen);

            // Act
            var result = await _artsController.GetArtsen();

            // Assert
            var artsenResult = Assert.IsType<List<Arts>>(result.Value);
            Assert.Equal(expectedArtsen.Count, artsenResult.Count);
        }

        [Fact]
        public async Task CreateArts_ReturnsCreatedAtAction_WhenArtsIsCreated()
        {
            // Arrange
            var newArts = new Arts { naam = "NewArts", id = "3" };

            // Act
            var result = await _artsController.CreateArts(newArts);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(newArts, createdAtActionResult.Value);
        }

        [Fact]
        public async Task CreateArts_ReturnsConflict_WhenArtsAlreadyExists()
        {
            // Arrange
            var newArts = new Arts { naam = "ExistingArts", id = "4" };
            _mockArtsRepository.Setup(x => x.AddArtsAsync(It.IsAny<Arts>())).ThrowsAsync(new Exception("Arts already exists"));

            // Act
            var result = await _artsController.CreateArts(newArts);

            // Assert
            Assert.IsType<ConflictObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateArts_ReturnsCreatedAtAction_WhenArtsIsUpdated()
        {
            // Arrange
            string artsId = "5";
            var updatedArts = new Arts { naam = "UpdatedArts", id = artsId };

            // Act
            var result = await _artsController.UpdateArts(artsId, updatedArts);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(updatedArts, createdAtActionResult.Value);
        }

        [Fact]
        public async Task DeleteArts_ReturnsOk_WhenArtsIsDeleted()
        {
            // Arrange
            string artsNaam = "TestArts";
            var existingArts = new Arts { naam = artsNaam, id = "6" };
            _mockArtsRepository.Setup(x => x.GetArtsByNaamAsync(artsNaam)).ReturnsAsync(existingArts);

            // Act
            var result = await _artsController.DeleteArts(artsNaam);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(artsNaam, okResult.Value);
        }

        [Fact]
        public async Task DeleteArts_ReturnsNotFound_WhenArtsDoesNotExist()
        {
            // Arrange
            string artsNaam = "NonExistentArts";
            _mockArtsRepository.Setup(x => x.GetArtsByNaamAsync(artsNaam)).ReturnsAsync((Arts)null);

            // Act
            var result = await _artsController.DeleteArts(artsNaam);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}