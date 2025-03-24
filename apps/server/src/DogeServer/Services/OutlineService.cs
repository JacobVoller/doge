using DogeServer.Data;
using DogeServer.Models.DogeResponses;
using DogeServer.Models.Entities;

namespace DogeServer.Services;

public interface IOutlineService
{
    Task<DogeResponse<List<Outline>>> GetTitles();
    Task<DogeResponse<List<Outline>>> GetChapters();
    Task<DogeResponse<int>> Count();
}

public class OutlineService() : IOutlineService
{
    protected readonly DataLake DataLake = DataLakeUtil.Factory();

    public async Task<DogeResponse<List<Outline>>> GetTitles()
    {
        var titles = await DataLake.Outline.GetTitles();

        return new DogeResponse<List<Outline>>()
        {
            Results = titles
        };
    }

    public async Task<DogeResponse<int>> Count()
    {
        var outlines = await DataLake.Outline.GetAll();
        var count = outlines?.Count ?? 0;

        return new DogeResponse<int>()
        {
            Results = count
        };
    }

    public async Task<DogeResponse<List<Outline>>> GetChapters()
    {
        var titles = await DataLake.Outline.GetChapters();

        return new DogeResponse<List<Outline>>()
        {
            Results = titles
        };
    }
}
