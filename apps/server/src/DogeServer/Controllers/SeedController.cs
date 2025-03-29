using DogeServer.Services.Seed;
using DogeServer.Util;
using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Controllers;

[ApiController]
[Route("")]
public class SeedController : ControllerBase
{
    protected readonly ISeedService _service;

    public SeedController() : base()
    {
        _service = new SeedService();
    }

    [HttpPost("seed")]
    public IActionResult Load()
    {
        var result = _service.StartSeed();
        return DogeServiceResponse.GenerateControllerResponse(result);
    }
}
