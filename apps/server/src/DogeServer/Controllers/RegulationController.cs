using DogeServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Controllers
{
    [ApiController]
    [Route("regulations")]
    public class RegulationController : ControllerBase
    {

        private readonly ILogger<RegulationController> _logger;
        protected readonly RegulationService _service;

        public RegulationController(ILogger<RegulationController> logger)
        {
            _logger = logger;
            _service = new RegulationService();
        }

        [HttpGet("query")]
        public async Task<IActionResult> Query()
        {


            await _service.Get();




            return Ok(new { Message = "This is the /x/me endpoint" });
        }

        [HttpPost("load")]
        public async Task<IActionResult> Load()
        {

            await _service.Get();

            return Ok(new { Message = "This is the /x/me endpoint" });
        }
    }
}
