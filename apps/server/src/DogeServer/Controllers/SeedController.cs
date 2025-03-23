using DogeServer.Data;
using DogeServer.Services;
using DogeServer.Util;
using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Controllers;

[ApiController]
[Route("")]
public class SeedController : ControllerBase
{
    protected readonly DataLake _dataLake;
    protected readonly ISeedService _service;

    public SeedController() : base()
    {
        _dataLake = new();
        _service = new SeedService(_dataLake);
    }

    [HttpPost("seed")]
    public async Task<IActionResult> Load()
    {
        return await DogeServiceResponse.GenerateControllerResponse(()
            => _service.StartSeed());
    }
}
