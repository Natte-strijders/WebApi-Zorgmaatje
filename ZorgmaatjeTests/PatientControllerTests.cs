using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ZorgmaatjeWebApi.Patient.Controllers;
using ZorgmaatjeWebApi.Patient.Repositories;

namespace ZorgmaatjeWebApi.Patient.Tests
{
    public class PatientControllerTests
    {
        private readonly Mock<IPatientRepository> _mockPatientRepository;
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly Mock<ILogger<PatientController>> _mockLogger;
        private readonly PatientController _patientController;

        public PatientControllerTests()
        {
            _mockPatientRepository = new Mock<IPatientRepository>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockLogger = new Mock<ILogger<PatientController>>();
            _patientController = new PatientController(_mockPatientRepository.Object, _mockAuthenticationService.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetUserId_ReturnsOkWithUserId()
        {
            // Arrange
            string expectedUserId = "testUserId";
            _mockAuthenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(expectedUserId);

            // Act
            var result = _patientController.GetUserId();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedUserId, okResult.Value);
        }

        [Fact]
        public async Task GetPatient_ReturnsOkWithPatient_WhenPatientExists()
        {
            // Arrange
            string patientId = "testPatientId";
            var expectedPatient = new Patient { id = patientId, voornaam = "John", achternaam = "Doe" };
            _mockPatientRepository.Setup(x => x.GetPatientByIdAsync(patientId)).ReturnsAsync(expectedPatient);

            // Act
            var result = await _patientController.GetPatient(patientId);

            // Assert
            var patientResult = Assert.IsType<Patient>(result.Value);
            Assert.Equal(expectedPatient, patientResult);
        }

        [Fact]
        public async Task GetPatient_ReturnsNotFound_WhenPatientDoesNotExist()
        {
            // Arrange
            string patientId = "nonExistentId";
            _mockPatientRepository.Setup(x => x.GetPatientByIdAsync(patientId)).ReturnsAsync((Patient)null);

            // Act
            var result = await _patientController.GetPatient(patientId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetPatients_ReturnsOkWithListOfPatients()
        {
            // Arrange
            var expectedPatients = new List<Patient>
            {
                new Patient { id = "1", voornaam = "John", achternaam = "Doe" },
                new Patient { id = "2", voornaam = "Jane", achternaam = "Smith" }
            };
            _mockPatientRepository.Setup(x => x.GetAllPatientsAsync()).ReturnsAsync(expectedPatients);

            // Act
            var result = await _patientController.GetPatients();

            // Assert
            var patientsResult = Assert.IsType<List<Patient>>(result.Value);
            Assert.Equal(expectedPatients.Count, patientsResult.Count);
        }

        [Fact]
        public async Task CreatePatient_ReturnsCreatedAtAction_WhenPatientIsCreated()
        {
            // Arrange
            var newPatient = new Patient { voornaam = "New", achternaam = "Patient" };
            string userId = "testUserId";
            _mockAuthenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userId);

            // Act
            var result = await _patientController.CreatePatient(newPatient);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(newPatient, createdAtActionResult.Value);
        }

        [Fact]
        public async Task CreatePatient_ReturnsConflict_WhenPatientAlreadyExists()
        {
            // Arrange
            var newPatient = new Patient { voornaam = "Existing", achternaam = "Patient" };
            string userId = "testUserId";
            _mockAuthenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userId);
            _mockPatientRepository.Setup(x => x.AddPatientAsync(It.IsAny<Patient>())).ThrowsAsync(new Exception("Patient already exists"));

            // Act
            var result = await _patientController.CreatePatient(newPatient);

            // Assert
            Assert.IsType<ConflictObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdatePatient_ReturnsCreatedAtAction_WhenPatientIsUpdated()
        {
            // Arrange
            string patientId = "testPatientId";
            var existingPatient = new Patient { id = patientId, voornaam = "Old", achternaam = "Name" };
            var updatedPatient = new Patient { id = patientId, voornaam = "New", achternaam = "Name" };
            _mockPatientRepository.Setup(x => x.GetPatientByIdAsync(patientId)).ReturnsAsync(existingPatient);

            // Act
            var result = await _patientController.UpdatePatient(patientId, updatedPatient);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(updatedPatient, createdAtActionResult.Value);
        }

        [Fact]
        public async Task UpdatePatient_ReturnsNotFound_WhenPatientDoesNotExist()
        {
            // Arrange
            string patientId = "nonExistentId";
            var updatedPatient = new Patient { id = patientId, voornaam = "New", achternaam = "Name" };
            _mockPatientRepository.Setup(x => x.GetPatientByIdAsync(patientId)).ReturnsAsync((Patient)null);

            // Act
            var result = await _patientController.UpdatePatient(patientId, updatedPatient);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeletePatient_ReturnsOk_WhenPatientIsDeleted()
        {
            // Arrange
            string patientId = "testPatientId";
            var existingPatient = new Patient { id = patientId, voornaam = "Old", achternaam = "Name" };
            _mockPatientRepository.Setup(x => x.GetPatientByIdAsync(patientId)).ReturnsAsync(existingPatient);

            // Act
            var result = await _patientController.DeletePatient(patientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(patientId, okResult.Value);
        }

        [Fact]
        public async Task DeletePatient_ReturnsNotFound_WhenPatientDoesNotExist()
        {
            // Arrange
            string patientId = "nonExistentId";
            _mockPatientRepository.Setup(x => x.GetPatientByIdAsync(patientId)).ReturnsAsync((Patient)null);

            // Act
            var result = await _patientController.DeletePatient(patientId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}