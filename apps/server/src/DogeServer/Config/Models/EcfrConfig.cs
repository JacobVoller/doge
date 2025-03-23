namespace DogeServer.Config.Models;

public class EcfrConfig
{
    public int ConcurrentRequests { get; set; } = 5;
    public string BaseUrl { get; set; } = "";
}
