using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogeServer.Models.Entities
{
    public class Section : StructureEntitity
    {
        public Guid? TitleID { get; set; }

        [ForeignKey("TitleID")]
        public virtual Title? ParentTitle { get; set; }

        [JsonProperty("date")] 
        public string? Date;

        [JsonProperty("amendment_date")] 
        public string? AmendmentDate;

        [JsonProperty("issue_date")] 
        public string? IssueDate;

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
    }
}
