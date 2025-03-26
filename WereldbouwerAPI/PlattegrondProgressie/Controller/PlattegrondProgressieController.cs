//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using ZorgmaatjeWebApi.Patient.Repositories;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using ZorgmaatjeWebApi.PlattegrondProgressie.Repositories;
//using ZorgmaatjeWebApi.PlattegrondProgressie;

//namespace ZorgmaatjeWebApi.PlattegrondProgressie.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class PlattegrondProgressieController : ControllerBase
//    {
//        private readonly IPlattegrondProgressieRepository _plattegrondProgressieRepository;
//        private readonly ILogger<PlattegrondProgressieController> _logger;

//        public PlattegrondProgressieController(IPlattegrondProgressieRepository repository, ILogger<PlattegrondProgressieController> logger)
//        {
//            _plattegrondProgressieRepository = repository;
//            _logger = logger;
//        }

//        [HttpGet("{id}")]
//        [Authorize]
//        public async Task<ActionResult<PlattegrondProgressie>> GetPlattegrondProgressie(int id)
//        {
//            var plattegrondProgressie = await _plattegrondProgressieRepository.GetPlattegrondProgressieByIdAsync(id);
//            if (plattegrondProgressie == null)
//            {
//                return NotFound();
//            }
//            return plattegrondProgressie;
//        }

//        [HttpGet]
//        [Authorize]
//        public async Task<ActionResult<IEnumerable<PlattegrondProgressie>>> GetPlattegrondProgressies()
//        {
//            var plattegrondProgressies = await _plattegrondProgressieRepository.GetAllPlattegrondProgressiesAsync();
//            return plattegrondProgressies.ToList();
//        }

//        [HttpPost]
//        [Authorize]
//        public async Task<ActionResult<PlattegrondProgressie>> CreatePlattegrondProgressie(PlattegrondProgressie plattegrondProgressie)
//        {
//            try
//            {
//                await _plattegrondProgressieRepository.AddPlattegrondProgressieAsync(plattegrondProgressie);
//                return CreatedAtAction(nameof(GetPlattegrondProgressie), new { plattegrondProgressie.ID }, plattegrondProgressie);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error creating PlattegrondProgressie");
//                return StatusCode(500, "An unexpected error occurred.");
//            }
//        }

//        [HttpPut("{id}")]
//        [Authorize]
//        public async Task<IActionResult> UpdatePlattegrondProgressie(int id, PlattegrondProgressie newPlattegrondProgressie)
//        {
//            var existingPlattegrondProgressie = await _plattegrondProgressieRepository.GetPlattegrondProgressieByIdAsync(id);
//            if (existingPlattegrondProgressie == null)
//            {
//                return NotFound();
//            }
//            newPlattegrondProgressie.ID = id;
//            await _plattegrondProgressieRepository.UpdatePlattegrondProgressieAsync(newPlattegrondProgressie);
//            return CreatedAtAction(nameof(GetPlattegrondProgressie), new { newPlattegrondProgressie.ID }, newPlattegrondProgressie);
//        }

//        [HttpDelete("{id}")]
//        [Authorize]
//        public async Task<IActionResult> DeletePlattegrondProgressie(int id)
//        {
//            var existingPlattegrondProgressie = await _plattegrondProgressieRepository.GetPlattegrondProgressieByIdAsync(id);
//            if (existingPlattegrondProgressie == null)
//            {
//                return NotFound();
//            }
//            await _plattegrondProgressieRepository.DeletePlattegrondProgressieAsync(id);
//            return Ok(id);
//        }
//    }
//}