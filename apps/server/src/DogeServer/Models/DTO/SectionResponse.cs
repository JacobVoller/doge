using DogeServer.Models.Entities;

namespace DogeServer.Models.DTO
{
    public class SectionResponse
    {
        public Section[]? content_versions { get; set; }
        public SectionMeta? meta { get; set; }
    }

    public class SectionMeta
    {
        public string? title;
        public string? result_count;
        /// <br/>    "issue_date": {
        /// <br/>      "lte": "2018-01-01",
        /// <br/>      "gte": "2017-01-01"
        /// <br/>    },
        public string? latest_amendment_date;
        public string? latest_issue_date;
    }

}
