using DogeServer.Data;
using DogeServer.Models;
using DogeServer.Util;

namespace DogeServer.Services;

public interface IQueryService
{
    Task<DogeServiceControllerResponse<QueryResponse>> Query(QueryRequest? request);
}

public class QueryService(DataLake dataLake) : IQueryService
{
    protected readonly DataLake DataLake = dataLake;

    public async Task<DogeServiceControllerResponse<QueryResponse>> Query(QueryRequest? request = null)
    {
        var input = CleanseRequest(request);
        var output = NewResponse(input);

        if (output == null || output.Results == null)
            throw new Exception("QueryService failed to initialize properly.");

        output.Results.TableData = await FilterRegulations(input);
        output.Results.Count = output.Results.Count;
        return output;
    }

    protected QueryRequest CleanseRequest(QueryRequest? request = null)
    {
        request ??= new QueryRequest();
        request.Keywords = CleanseSingleFilter(request.Keywords);
        request.Agency = CleanseSingleFilter(request.Agency);

        return request;
    }

    protected string? CleanseSingleFilter(string? filter)
    {
        if (filter == null) return null;

        var cleansed = filter.Trim().ToLower();
        return string.IsNullOrWhiteSpace(cleansed)
            ? null
            : cleansed;
    }

    protected DogeServiceControllerResponse<QueryResponse> NewResponse(QueryRequest filters)
    {
        return new DogeServiceControllerResponse<QueryResponse>
        {
            Results = new QueryResponse
            {
                Filters = ReflectionUtil.DeepClone(filters)
            }
        };
    }

    protected async Task<List<string[]>> FilterRegulations(QueryRequest filters)
    {
        var results = new List<string[]>();
        var filterByKeyword = !string.IsNullOrEmpty(filters.Keywords);
        var filterByAgency = !string.IsNullOrEmpty(filters.Agency);

        if (filterByKeyword)
        {
        }

        if (filterByAgency)
        {
        }

        //TODO!!

        return results;
    }
}
