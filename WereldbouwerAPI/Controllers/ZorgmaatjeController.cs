using Microsoft.AspNetCore.Mvc;
using ZorgmaatjeWebApi;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace ZorgmaatjeWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZorgmaatjeController : ControllerBase
    {
        private readonly IZorgmaatjeRepository _zorgmaatjeRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<ZorgmaatjeController> _logger;

        public ZorgmaatjeController(IZorgmaatjeRepository repository, IAuthenticationService authenticationService, ILogger<ZorgmaatjeController> logger)
        {
            _zorgmaatjeRepository = repository;
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
