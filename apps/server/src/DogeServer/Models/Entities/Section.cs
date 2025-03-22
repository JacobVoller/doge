using Newtonsoft.Json;

namespace DogeServer.Models.Entities
{
    public class Section : Entity
    {
        [JsonProperty("date")] 
        public string? Date;

        [JsonProperty("amendment_date")] 
        public string? AmendmentDate;

        [JsonProperty("issue_date")] 
        public string? IssueDate;

        [JsonProperty("identifier")] 
        public string? Identifier;

        [JsonProperty("name")] 
        public string? Name;

        [JsonProperty("part")] 
        public string? Part;

        [JsonProperty("substantive")] 
        public bool? Substantive;

        [JsonProperty("removed")] 
        public bool? Removed;

        [JsonProperty("subpart")] 
        public string? Subpart;

        [JsonProperty("title")] 
        public string? Title;

        [JsonProperty("type")] 
        public string? Type;
    }
}
