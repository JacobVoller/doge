namespace DogeServer.Models;

public class QueryResponse
{
    public QueryRequest? Filters { get; set; }
    public string[]? Columns { get; set; }
    public int? Count { get; set; }
    public List<string[]>? TableData { get; set; }
}
