namespace DogeServer.Util
{
    public static class DebugUtil
    {
        public static void Log(string operation, string status)
        {
            try
            {
                operation = operation.PadLeft(10, ' ');
                status = (status + " ").PadRight(10, '-');
                string line = new('-', 10);

                Console.WriteLine($">  DogeServer ▸ [{operation}] {status}{line}");
            }
            catch { }
        }
    }
}
