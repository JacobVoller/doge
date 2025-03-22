using Newtonsoft.Json;

namespace DogeServer.Models.Entities
{
    public class Title : Entity
    {
        [JsonProperty("number")] 
        public int? Number { get; set; }

        [JsonProperty("name")] 
        public string? Name { get; set; }

        [JsonProperty("latest_amended_on")] 
        public string? LastAmended { get; set; }

        [JsonProperty("latest_issue_date")] 
        public string? LastIssued { get; set; }

        [JsonProperty("up_to_date_as_of")] 
        public string? LastUpdated { get; set; }

        [JsonProperty("reserved")] 
        public bool? Reserved { get; set; }

        [JsonProperty("processing_in_progress")] 
        public bool? ProcessingInProgress { get; set; }
    }
}
