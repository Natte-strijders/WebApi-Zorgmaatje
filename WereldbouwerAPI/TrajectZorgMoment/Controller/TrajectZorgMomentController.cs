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

        [HttpGet("{id}")]
        public async Task<ActionResult<TrajectZorgMoment>> GetTrajectZorgMoment(int id)
        {
            var trajectZorgMoment = await _trajectZorgMomentRepository.GetByIdAsync(id);

            if (trajectZorgMoment == null)
            {
                return NotFound();
            }

            return trajectZorgMoment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrajectZorgMoment>>> GetTrajectZorgMomenten()
        {
            var trajectZorgMomenten = await _trajectZorgMomentRepository.GetAllAsync();
            return trajectZorgMomenten.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<TrajectZorgMoment>> PostTrajectZorgMoment(TrajectZorgMoment trajectZorgMomentDto)
        {
            var trajectZorgMoment = new TrajectZorgMoment
            {
                ZorgMomentId = trajectZorgMomentDto.ZorgMomentId,
                Volgorde = trajectZorgMomentDto.Volgorde
            };

            await _trajectZorgMomentRepository.AddAsync(trajectZorgMoment);

            return CreatedAtAction(nameof(GetTrajectZorgMoment), new { id = trajectZorgMoment.TrajectZorgMomentId }, trajectZorgMoment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrajectZorgMoment(int id, TrajectZorgMoment trajectZorgMomentDto)
        {
            var existingTrajectZorgMoment = await _trajectZorgMomentRepository.GetByIdAsync(id);

            if (existingTrajectZorgMoment == null)
            {
                return NotFound();
            }

            existingTrajectZorgMoment.ZorgMomentId = trajectZorgMomentDto.ZorgMomentId;
            existingTrajectZorgMoment.Volgorde = trajectZorgMomentDto.Volgorde;

            await _trajectZorgMomentRepository.UpdateAsync(existingTrajectZorgMoment);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrajectZorgMoment(int id)
        {
            await _trajectZorgMomentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}