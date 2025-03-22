using DogeServer.Models.Entities;
using Newtonsoft.Json;

namespace DogeServer.Models.DTO;

public class TitleResponse
{
    [JsonProperty("titles")]
    public Title[]? Titles { get; set; }

    [JsonProperty("meta")] 
    public TitlesMeta? Meta { get; set; }
}

public class TitlesMeta
{
    [JsonProperty("date")] 
    public DateTime Date { get; set; }

    [JsonProperty("import_in_progress")] 
    public bool ImportInProgress { get; set; }
}
