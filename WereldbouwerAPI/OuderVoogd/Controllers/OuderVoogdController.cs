using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ZorgmaatjeWebApi.OuderVoogd.Repositories;
using ZorgmaatjeWebApi.Patient;

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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OuderVoogd>> GetOuderVoogd(string id)
        {
            var ouderVoogd = await _ouderVoogdRepository.GetOuderVoogdByIdAsync(id);
            if (ouderVoogd == null)
            {
                return NotFound();
            }
            return ouderVoogd;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OuderVoogd>>> GetOuderVoogden()
        {
            var ouderVoogden = await _ouderVoogdRepository.GetAllOuderVoogdenAsync();
            return ouderVoogden.ToList();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OuderVoogd>> CreateOuderVoogd(OuderVoogd ouderVoogd)
        {
            ouderVoogd.id = Guid.NewGuid().ToString();
            await _ouderVoogdRepository.AddOuderVoogdAsync(ouderVoogd);
            return CreatedAtAction(nameof(GetOuderVoogd), new { id = ouderVoogd.id }, ouderVoogd);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateOuderVoogd(string id, OuderVoogd newOuderVoogd)
        {
            var existingOuderVoogd = await _ouderVoogdRepository.GetOuderVoogdByIdAsync(id);
            if (existingOuderVoogd == null)
            {
                return NotFound();
            }
            newOuderVoogd.id = id;
            await _ouderVoogdRepository.UpdateOuderVoogdAsync(newOuderVoogd);
            return CreatedAtAction(nameof(GetOuderVoogd), new { newOuderVoogd.id }, newOuderVoogd);

        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOuderVoogd(string id)
        {
            var existingOuderVoogd = await _ouderVoogdRepository.GetOuderVoogdByIdAsync(id);
            if (existingOuderVoogd == null)
            {
                return NotFound();
            }
            await _ouderVoogdRepository.DeleteOuderVoogdAsync(id);
            return Ok(id);
        }

    }
}
