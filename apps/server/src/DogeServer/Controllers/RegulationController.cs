using DogeServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Controllers
{
    [ApiController]
    [Route("regulations")]
    public class RegulationController(ILogger<RegulationController> logger) : ControllerBase
    {
        private readonly ILogger<RegulationController> _logger = logger;

        [HttpGet("query")]
        public async Task<IActionResult> Query()
        {
            //TODO
            return Ok(new { Message = "This is the /x/me endpoint" });
        }

        [HttpPost("load")]
        public async Task<IActionResult> Load()
        {
            var service = new DataRetrievalService();
            await service.Load();

            return Ok(new { Message = "This is the /x/me endpoint" });
        }
    }
}
