﻿using DogeServer.Models.DogeRequests;

namespace DogeServer.Models.DogeResponses;

public class QueryRegulationsResponse
{
    public QueryRequest? Filters { get; set; }
    public string[]? Columns { get; set; }
    public int? Count { get; set; }
    public List<string[]>? TableData { get; set; }
}
