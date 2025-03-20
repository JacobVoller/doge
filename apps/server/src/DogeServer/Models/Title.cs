namespace DogeServer.Models
{
    public class Title
    {
        public int? number { get; set; }
        public string? name { get; set; }
        public DateTime? latest_amended_on { get; set; }
        public DateTime? latest_issue_date { get; set; }
        public DateTime? up_to_date_as_of { get; set; }
        public bool? reserved { get; set; }      
        public bool? processing_in_progress { get; set; }
    }
}
