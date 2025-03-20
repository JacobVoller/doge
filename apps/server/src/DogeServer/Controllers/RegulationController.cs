using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegulationController : ControllerBase
    {

        private readonly ILogger<RegulationController> _logger;

        public RegulationController(ILogger<RegulationController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRegulations")]
        public string Get()
        {
            return "OK";
        }

        [HttpPost(Name = "load")]
        public string Post()
        {
            return "OK";
        }
    }
}
