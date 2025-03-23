using Newtonsoft.Json;

namespace DogeServer.Models.Entities;

public class Outline : Entity
{
    [JsonProperty("amendment_date")]
    public string? AmendmentDate { get; set; }

    [JsonProperty("date")]
    public string? Date { get; set; }

    [JsonProperty("descendant_range")]
    public string? DescendantRange { get; set; }

    [JsonProperty("identifier")]
    public string? Identifier { get; set; }

    [JsonProperty("issue_date")]
    public string? IssueDate { get; set; }

    [JsonProperty("label")]
    public string? Label { get; set; }

    [JsonProperty("label_description")]
    public string? LabelDescription { get; set; }

    [JsonProperty("label_level")]
    public string? LabelLevel { get; set; }

    [JsonProperty("latest_amended_on")]
    public string? LastAmended { get; set; }

    [JsonProperty("latest_issue_date")]
    public string? LastIssued { get; set; }

    [JsonProperty("up_to_date_as_of")]
    public string? LastUpdated { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("number")]
    public int? Number { get; set; }

    [JsonProperty("part")]
    public string? Part { get; set; }

    [JsonProperty("processing_in_progress")]
    public bool? ProcessingInProgress { get; set; }

    [JsonProperty("received_on")]
    public string? ReceivedOn { get; set; }

    [JsonProperty("removed")]
    public bool? Removed { get; set; }

    [JsonProperty("reserved")]
    public bool? Reserved { get; set; }

    [JsonProperty("section_range")]
    public string? SectionRange { get; set; }

    [JsonProperty("size")]
    public int? Size { get; set; }

    [JsonProperty("subpart")]
    public string? Subpart { get; set; }

    [JsonProperty("substantive")]
    public bool? Substantive { get; set; }

    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("volumes")]
    public string[]? Volumes { get; set; }

}
