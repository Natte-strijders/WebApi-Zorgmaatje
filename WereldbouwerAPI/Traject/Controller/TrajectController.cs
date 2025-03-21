using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ZorgmaatjeWebApi.Patient.Repositories;
using ZorgmaatjeWebApi.Traject.Repositories;

namespace ZorgmaatjeWebApi.Traject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrajectController : ControllerBase
    {
        private readonly ITrajectRepository _trajectRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<TrajectController> _logger;

        public TrajectController(ITrajectRepository repository, IAuthenticationService authenticationService, ILogger<TrajectController> logger)
        {
            _trajectRepository = repository;
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpGet("{naam}")]
        [Authorize]
        public async Task<ActionResult<Traject>> GetTraject(string naam)
        {
            var traject = await _trajectRepository.GetTrajectByNaamAsync(naam);
            if (traject == null)
            {
                return NotFound();
            }
            return traject;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Traject>>> GetTrajects()
        {
            var trajects = await _trajectRepository.GetAllTrajectsAsync();
            return trajects.ToList();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Traject>> CreateTraject(Traject traject)
        {
            try
            {
                await _trajectRepository.AddTrajectAsync(traject);
                return CreatedAtAction(nameof(GetTraject), new { naam = traject.naam }, traject);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("already exists"))
                {
                    return Conflict(ex.Message);
                }
                else
                {
                    return StatusCode(500, "An unexpected error occurred.");
                }
            }
        }

        [HttpPut("{naam}")]
        [Authorize]
        public async Task<IActionResult> UpdateTraject(string naam, Traject newTraject)
        {
            _logger.LogInformation($"UpdateTraject called with naam: {naam}");
            var existingTraject = await _trajectRepository.GetTrajectByNaamAsync(naam);
            if (existingTraject == null)
            {
                return NotFound();
            }
            newTraject.id = existingTraject.id;
            await _trajectRepository.UpdateTrajectAsync(newTraject);
            return CreatedAtAction(nameof(GetTraject), new { naam = newTraject.naam }, newTraject);
        }

        [HttpDelete("{naam}")]
        [Authorize]
        public async Task<IActionResult> DeleteTraject(string naam)
        {
            _logger.LogInformation($"DeleteTraject called with naam: {naam}");
            var existingTraject = await _trajectRepository.GetTrajectByNaamAsync(naam);
            if (existingTraject == null)
            {
                _logger.LogWarning($"Traject with naam: {naam} not found");
                return NotFound();
            }
            await _trajectRepository.DeleteTrajectAsync(naam);
            return Ok(naam);
        }

    }
}
