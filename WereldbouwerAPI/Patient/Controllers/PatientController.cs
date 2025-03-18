using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ZorgmaatjeWebApi.Patient.Repositories;

namespace ZorgmaatjeWebApi.Patient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<PatientController> _logger;

        public PatientController(IPatientRepository repository, IAuthenticationService authenticationService, ILogger<PatientController> logger)
        {
            _patientRepository = repository;
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpGet("GetUserId", Name = "GetUserId")]
        [Authorize]
        public ActionResult<string> GetUserId()
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            return Ok(userId);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Patient>> GetPatient(string id)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return patient;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _patientRepository.GetAllPatientsAsync();
            return patients.ToList();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            await _patientRepository.AddPatientAsync(patient);
            return CreatedAtAction(nameof(GetPatient), new { patient.id }, patient);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePatient(string id, Patient patient)
        {
            if (id != patient.id)
            {
                return BadRequest();
            }
            await _patientRepository.UpdatePatientAsync(patient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePatient(string id)
        {
            await _patientRepository.DeletePatientAsync(id);
            return NoContent();
        }

    }
}
