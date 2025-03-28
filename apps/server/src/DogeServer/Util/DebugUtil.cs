namespace DogeServer.Util;

public static class DebugUtil
{
    public static void Log(string? msg)
    {
        if (string.IsNullOrEmpty(msg)) return;

        try
        {
            Console.WriteLine($">  DogeServer | {msg}");
        }
        catch { }
    }
    
    public static void Log(string operation, string? status)
    {
        status ??= string.Empty;
        
        try
        {
            operation = operation.PadLeft(10, ' ');
            status = (status + " ").PadRight(10, '-');
            string line = new('-', 10);

            var msg = $"[{operation}] {status}{line}";
            Log(msg);
        }
        catch { }
    }
}
