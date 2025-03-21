using DogeServer.Models.Entities;

namespace DogeServer.Models.DTO
{
    public class TitleResponse
    {
        public Title[]? titles { get; set; }
        public TitlesMeta? meta { get; set; }
    }

    public class TitlesMeta
    {
        public DateTime date { get; set; }
        public bool import_in_progress { get; set; }
    }
}
