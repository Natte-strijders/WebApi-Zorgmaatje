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
    }
}