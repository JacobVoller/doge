using DogeServer.Data;
using DogeServer.Models.DogeRequests;
using DogeServer.Services;
using DogeServer.Util;
using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Controllers;

[ApiController]
[Route("regulations")]
public class RegulationsController() : ControllerBase
{
    protected readonly DataLake _dataLake = new();

    [HttpGet("query")]
    public async Task<IActionResult> Query(QueryRequest request)
    {
        IQueryRegulationsService service = new QueryRegulationsService(_dataLake);
        return await DogeServiceResponse.GenerateControllerResponse(() => service.Query(request));
    }
}
