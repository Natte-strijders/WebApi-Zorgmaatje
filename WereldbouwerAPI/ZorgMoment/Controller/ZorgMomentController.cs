using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgmaatjeWebApi.ZorgMoment.Repositories;

namespace ZorgmaatjeWebApi.ZorgMoment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZorgMomentController : ControllerBase
    {
        private readonly IZorgMomentRepository _zorgMomentRepository;

        public ZorgMomentController(IZorgMomentRepository zorgMomentRepository)
        {
            _zorgMomentRepository = zorgMomentRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ZorgMoment>> GetZorgMoment(int id)
        {
            var zorgMoment = await _zorgMomentRepository.GetByIdAsync(id);

            if (zorgMoment == null)
            {
                return NotFound();
            }

            return zorgMoment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZorgMoment>>> GetZorgMomenten()
        {
            var zorgMomenten = await _zorgMomentRepository.GetAllAsync();
            return zorgMomenten.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<ZorgMoment>> PostZorgMoment(ZorgMoment zorgMoment)
        {
            await _zorgMomentRepository.AddAsync(zorgMoment);
            return CreatedAtAction(nameof(GetZorgMoment), new { id = zorgMoment.id }, zorgMoment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutZorgMoment(int id, ZorgMoment zorgMoment)
        {
            if (id != zorgMoment.id)
            {
                return BadRequest();
            }

            await _zorgMomentRepository.UpdateAsync(zorgMoment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZorgMoment(int id)
        {
            await _zorgMomentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}