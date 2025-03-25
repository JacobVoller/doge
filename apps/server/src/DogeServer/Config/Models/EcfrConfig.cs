namespace DogeServer.Config.Models;

public class EcfrConfig
{
    public int ConcurrentJsonRequests { get; set; } = 5;
    public int ConcurrentXmlRequests { get; set; } = 1;
    public string BaseUrl { get; set; } = "";
}
