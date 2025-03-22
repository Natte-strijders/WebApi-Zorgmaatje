using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ZorgmaatjeWebApi.Arts.Repositories;
using ZorgmaatjeWebApi.Patient.Controllers;
using ZorgmaatjeWebApi.Patient.Repositories;

namespace ZorgmaatjeWebApi.Arts.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ArtsController : ControllerBase
    {
        private readonly IArtsRepository _artsRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<ArtsController> _logger;

        public ArtsController(IArtsRepository repository, IAuthenticationService authenticationService, ILogger<ArtsController> logger)
        {
            _artsRepository = repository;
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpGet("{naam}")]
        [Authorize]
        public async Task<ActionResult<Arts>> GetArts(string naam)
        {
            var arts = await _artsRepository.GetArtsByNaamAsync(naam);
            if (arts == null)
            {
                return NotFound();
            }
            return arts;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Arts>>> GetArtsen()
        {
            var artsen = await _artsRepository.GetAllArtsAsync();
            return artsen.ToList();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Arts>> CreateArts(Arts arts)
        {
            try
            {
                await _artsRepository.AddArtsAsync(arts);
                return CreatedAtAction(nameof(GetArts), new { naam = arts.naam }, arts);
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

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateArts(string id, Arts arts)
        {
            arts.id = id;
            await _artsRepository.UpdateArtsAsync(arts);
            return CreatedAtAction(nameof(GetArts), new { naam = arts.naam }, arts);
        }

        [HttpDelete("{naam}")]
        [Authorize]
        public async Task<IActionResult> DeleteArts(string naam)
        {
            var existingArts = await _artsRepository.GetArtsByNaamAsync(naam);
            if (existingArts == null)
            {
                return NotFound();
            }
            await _artsRepository.DeleteArtsAsync(naam);
            return Ok(naam);
        }
    }
}
