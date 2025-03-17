using Microsoft.AspNetCore.Mvc;
using ZorgmaatjeWebApi;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace ZorgmaatjeWebApi.Controllers
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
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _patientRepository.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return patient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _patientRepository.GetAllPatients();
            return patients.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            await _patientRepository.AddPatient(patient);
            return CreatedAtAction(nameof(GetPatient), new { id = patient.id }, patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.id)
            {
                return BadRequest();
            }
            await _patientRepository.UpdatePatient(patient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            await _patientRepository.DeletePatient(id);
            return NoContent();
        }

    }
}
