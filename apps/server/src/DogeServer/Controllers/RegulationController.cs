using DogeServer.Data;
using DogeServer.Models;
using DogeServer.Services;
using DogeServer.Util;
using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Controllers;

[ApiController]
[Route("regulations")]
public class RegulationController(ILogger<RegulationController> logger) : ControllerBase
{
    protected readonly ILogger<RegulationController> _logger = logger; //TODO
    protected readonly DataLake _dataLake = new();

    [HttpGet("query")]
    public async Task<IActionResult> Query(QueryRequest request)
    {
        IQueryService service = new QueryService(_dataLake);
        return await DogeServiceResponse.GenerateControllerResponse(() => service.Query(request));
    }

    [HttpPost("load")]
    public async Task<IActionResult> Load()
    {
        IDataRetrievalService service = new DataRetrievalService(_dataLake);
        return await DogeServiceResponse.GenerateControllerResponse(() => service.Load());
    }
}
