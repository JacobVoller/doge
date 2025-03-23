using DogeServer.Data;
using DogeServer.Models.DogeRequests;
using DogeServer.Services;
using DogeServer.Util;
using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Controllers;

[ApiController]
[Route("regulations")]
public class RegulationsController : ControllerBase
{
    protected readonly DataLake _dataLake;
    protected readonly IQueryRegulationsService _service;

    public RegulationsController() : base()
    {
        _dataLake = new();
        _service = new QueryRegulationsService(_dataLake);
    }

    [HttpGet("query")]
    public async Task<IActionResult> Query(QueryRequest request)
    {
        return await DogeServiceResponse.GenerateControllerResponse(() 
            => _service.Query(request));
    }
}
