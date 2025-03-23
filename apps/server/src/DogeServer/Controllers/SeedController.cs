using DogeServer.Data;
using DogeServer.Services;
using DogeServer.Util;
using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Controllers;

[ApiController]
[Route("")]
public class SeedController() : ControllerBase
{
    protected readonly DataLake _dataLake = new();

    [HttpPost("seed")]
    public async Task<IActionResult> Load()
    {
        ISeedService service = new SeedService(_dataLake);
        return await DogeServiceResponse.GenerateControllerResponse(() => service.StartSeed());
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetCount()
    {
        ISeedService service = new SeedService(_dataLake);
        return await DogeServiceResponse.GenerateControllerResponse(() => service.OutlineCount());
    }
}
