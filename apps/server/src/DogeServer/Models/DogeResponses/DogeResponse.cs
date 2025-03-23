namespace DogeServer.Models.DogeResponses;

public class DogeResponse<T>
{
    public T? Results { get; set; }
    public int? StatusCode { get; set; }
    public int? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
    public string? StackTrace { get; set; }
}
