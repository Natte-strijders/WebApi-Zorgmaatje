using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ZorgmaatjeWebApi.OuderVoogd.Repositories;

namespace ZorgmaatjeWebApi.OuderVoogd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OuderVoogdController : ControllerBase
    {
        private readonly IOuderVoogdRepository _ouderVoogdRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<OuderVoogdController> _logger;

        public OuderVoogdController(IOuderVoogdRepository repository, IAuthenticationService authenticationService, ILogger<OuderVoogdController> logger)
        {
            _ouderVoogdRepository = repository;
            _authenticationService = authenticationService;
            _logger = logger;
        }

        public async Task<ActionResult<OuderVoogd>> GetPatient(string id)
        {
            var patient = await _ouderVoogdRepository.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return patient;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OuderVoogd>>> GetPatients()
        {
            var patients = await _ouderVoogdRepository.GetAllPatientsAsync();
            return patients.ToList();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OuderVoogd>> CreatePatient(OuderVoogd patient)
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            patient.id = userId;
            await _ouderVoogdRepository.AddPatientAsync(patient);
            return CreatedAtAction(nameof(GetPatient), new { patient.id }, patient);
        }

    }
}
