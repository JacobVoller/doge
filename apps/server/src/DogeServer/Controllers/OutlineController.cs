using DogeServer.Data;
using DogeServer.Services;
using DogeServer.Util;
using Microsoft.AspNetCore.Mvc;

namespace DogeServer.Controllers;

[ApiController]
[Route("outline")]
public class OutlineController : ControllerBase
{
    protected readonly IOutlineService _service;

    public OutlineController() : base()
    {
        _service = new OutlineService();
    }

    [HttpGet("title")]
    public async Task<IActionResult> GetTitles()
    {
        return await DogeServiceResponse.GenerateControllerResponse(()
            => _service.GetTitles());
    }

    [HttpGet("chapter")]
    public async Task<IActionResult> GetChapters()
    {
        return await DogeServiceResponse.GenerateControllerResponse(()
            => _service.GetChapters());
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetTitleCount()
    {
        return await DogeServiceResponse.GenerateControllerResponse(()
            => _service.Count());
    }

}
