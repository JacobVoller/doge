using Newtonsoft.Json;

namespace DogeServer.Models.DTO;

public class SectionResponse
{
    //[JsonProperty("content_versions")]
    //public Section[]? ContentVersions { get; set; }
    
    [JsonProperty("meta")]
    public SectionMeta? Meta { get; set; }
}

public class SectionMeta
{
    [JsonProperty("title")] 
    public string? Title;

    [JsonProperty("result_count")]
    public string? ResultCount;
    
    // issue_date": {
    //    lte: "2018-01-01",
    //    gte: "2017-01-01"
    // }

    [JsonProperty("latest_amendment_date")]
    public string? LatestAmendmentDate;

    [JsonProperty("latest_issue_date")]
    public string? LatestIssueDate;
}