using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<ZorgMoment>> GetZorgMoment(int id)
        {
            var zorgMoment = await _zorgMomentRepository.GetByIdAsync(id);

            if (zorgMoment == null)
            {
                return NotFound();
            }

            return zorgMoment;
        }

        [HttpGet("name/patient/{naam}/{patientId}")]
        [Authorize]
        public async Task<IActionResult> GetZorgMomentByNameAndPatientId(string naam, string patientId)
        {
            var zorgMoment = await _zorgMomentRepository.GetZorgMomentByNameAndPatientIdAsync(naam, patientId);

            if (zorgMoment == null)
            {
                return NotFound();
            }

            return Ok(zorgMoment);
        }

        [HttpGet("patient/volgorde/{patientId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetZorgMomentenByPatientIdSortedByVolgorde(string patientId)
        {
            var zorgMomenten = await _zorgMomentRepository.GetZorgMomentenByPatientIdSortedByVolgordeAsync(patientId);
            return Ok(zorgMomenten);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ZorgMoment>>> GetZorgMomenten()
        {
            var zorgMomenten = await _zorgMomentRepository.GetAllAsync();
            return zorgMomenten.ToList();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ZorgMoment>> PostZorgMoment(ZorgMoment zorgMoment)
        {
            await _zorgMomentRepository.AddAsync(zorgMoment);
            return CreatedAtAction(nameof(GetZorgMoment), new { id = zorgMoment.id }, zorgMoment);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutZorgMoment(int id, ZorgMoment zorgMoment)
        {
            if (id != zorgMoment.id)
            {
                return BadRequest();
            }

            await _zorgMomentRepository.UpdateAsync(zorgMoment);
            return CreatedAtAction(nameof(GetZorgMoment), new { id = zorgMoment.id }, zorgMoment);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteZorgMoment(int id)
        {
            await _zorgMomentRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpDelete("Patient/{patientId}")]
        [Authorize]
        public async Task<IActionResult> DeleteZorgMomentenByPatientId(string patientId)
        {
            int affectedRows = await _zorgMomentRepository.DeleteZorgMomentenByPatientIdAsync(patientId);

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return Ok(affectedRows);
        }
    }
}