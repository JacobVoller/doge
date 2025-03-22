using DogeServer.Data;
using DogeServer.Models;
using DogeServer.Services;
using DogeServer.Util;
using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Controllers
{
    [ApiController]
    [Route("regulations")]
    public class RegulationController(ILogger<RegulationController> logger) : ControllerBase
    {
        private readonly ILogger<RegulationController> _logger = logger; //TODO

        [HttpGet("query")]
        public async Task<IActionResult> Query()
        {
            return await DogeServiceResponse.GenerateControllerResponse(() => Task.FromResult(new DogeServiceControllerResponse<string>
            {
                Results = "TODO"
            }));
        }

        [HttpPost("load")]
        public async Task<IActionResult> Load()
        {
            var dataLake = new DataLake();
            IDataRetrievalService retriever = new DataRetrievalService(dataLake);

            return await DogeServiceResponse.GenerateControllerResponse(() => retriever.Load());
        }
    }
}
