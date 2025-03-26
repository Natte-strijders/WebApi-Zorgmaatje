using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ZorgmaatjeWebApi.TrajectZorgMoment.Repositories;

namespace ZorgmaatjeWebApi.TrajectZorgMoment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrajectZorgMomentController : ControllerBase
    {
        private readonly ITrajectZorgMomentRepository _trajectZorgMomentRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<TrajectZorgMomentController> _logger;

        public TrajectZorgMomentController(ITrajectZorgMomentRepository repository, IAuthenticationService authenticationService, ILogger<TrajectZorgMomentController> logger)
        {
            _trajectZorgMomentRepository = repository;
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpGet("{trajectId}/{zorgMomentId}")]
        [Authorize]
        public async Task<ActionResult<TrajectZorgMoment>> GetTrajectZorgMoment(int trajectId, int zorgMomentId)
        {
            var key = new TrajectZorgMomentKey { TrajectId = trajectId, ZorgMomentId = zorgMomentId };
            var trajectZorgMoment = await _trajectZorgMomentRepository.GetByIdAsync(key);

            if (trajectZorgMoment == null)
            {
                return NotFound();
            }

            return Ok(trajectZorgMoment);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TrajectZorgMoment>>> GetTrajectZorgMomenten()
        {
            var trajectZorgMomenten = await _trajectZorgMomentRepository.GetAllAsync();
            return trajectZorgMomenten.ToList();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TrajectZorgMoment>> PostTrajectZorgMoment(TrajectZorgMoment trajectZorgMomentDto)
        {
            var trajectZorgMoment = new TrajectZorgMoment
            {
                trajectId = trajectZorgMomentDto.trajectId,
                zorgMomentId = trajectZorgMomentDto.zorgMomentId,
                volgorde = trajectZorgMomentDto.volgorde
            };

            await _trajectZorgMomentRepository.AddAsync(trajectZorgMoment);

            return CreatedAtAction(nameof(GetTrajectZorgMoment), new { trajectId = trajectZorgMoment.trajectId, zorgMomentId = trajectZorgMoment.zorgMomentId }, trajectZorgMoment);
        }

        [HttpPut("{trajectId}/{zorgMomentId}")]
        [Authorize]
        public async Task<IActionResult> PutTrajectZorgMoment(int trajectId, int zorgMomentId, TrajectZorgMoment trajectZorgMomentDto)
        {
            var key = new TrajectZorgMomentKey { TrajectId = trajectId, ZorgMomentId = zorgMomentId };
            var existingTrajectZorgMoment = await _trajectZorgMomentRepository.GetByIdAsync(key);

            if (existingTrajectZorgMoment == null)
            {
                return NotFound();
            }

            existingTrajectZorgMoment.volgorde = trajectZorgMomentDto.volgorde;

            await _trajectZorgMomentRepository.UpdateAsync(existingTrajectZorgMoment);

            return NoContent();
        }

        [HttpDelete("{trajectId}/{zorgMomentId}")]
        [Authorize]
        public async Task<IActionResult> DeleteTrajectZorgMoment(int trajectId, int zorgMomentId)
        {
            var key = new TrajectZorgMomentKey { TrajectId = trajectId, ZorgMomentId = zorgMomentId };
            await _trajectZorgMomentRepository.DeleteAsync(key);
            return NoContent();
        }

        [HttpDelete("Patient/{patientId}")]
        [Authorize]
        public async Task<IActionResult> DeleteTrajectZorgMomentenByPatientId(string patientId)
        {
            int affectedRows = await _trajectZorgMomentRepository.DeleteTrajectZorgMomentenByPatientIdAsync(patientId);

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return Ok(affectedRows);
        }
    }
}