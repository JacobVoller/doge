using Newtonsoft.Json;

namespace DogeServer.Models.DTO;

public class TitleStructure
{
    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("label")] 
    public string? Label { get; set; }

    [JsonProperty("label_level")] 
    public string? LabelLevel { get; set; }

    [JsonProperty("label_description")] 
    public string? LabelDescription { get; set; }

    [JsonProperty("identifier")] 
    public string? Identifier { get; set; }

    [JsonProperty("children")] 
    public TitleStructure[]? Children { get; set; }

    [JsonProperty("section_range")] 
    public string? SectionRange { get; set; }

    [JsonProperty("size")]
    public int? Size { get; set; }

    [JsonProperty("reserved")]
    public bool? Reserved { get; set; }

    [JsonProperty("volumes")]
    public string[]? Volumes { get; set; }

    [JsonProperty("received_on")]
    public string? ReceivedOn { get; set; }

}
