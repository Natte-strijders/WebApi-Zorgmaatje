using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ZorgmaatjeWebApi.Patient.Repositories;

namespace ZorgmaatjeWebApi.Traject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrajectController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<TrajectController> _logger;

        public TrajectController(IPatientRepository repository, IAuthenticationService authenticationService, ILogger<TrajectController> logger)
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


    }
}
