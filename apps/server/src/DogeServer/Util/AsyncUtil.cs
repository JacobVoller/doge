namespace DogeServer.Util;

public class AsyncUtil
{
    protected static Dictionary<string, ThreadUtil> Threads { get; set; } = [];

    public static ThreadUtil? RegisterThread(string key, Action<CancellationToken> action)
    {
        if (Threads.ContainsKey(key))
            return null;

        var thread = new ThreadUtil();
        var cancelToken = thread.CancelTrigger.Token;
        thread.Task = Task.Run(() => action(cancelToken), cancelToken);

        Threads[key] = thread;
        return thread;
    }

    public static void FireAndForget(Action action)
    {
        Task.Run(() => action());
    }

    public static async Task AwaitAllThreads()
    {
        if (Threads == null)
            return;
        await Task.WhenAll(GetAllThreads());
    }

    public static void Shutdown()
    {
        foreach (var key in Threads.Keys)
        {
            var thread = Threads[key];
            if (thread == null)
                continue;

            thread.Cancel();
        }
    }

    private static Task[] GetAllThreads()
    {
        return Threads
            .Select(thread => thread.Value)
            .Where(thread => thread != null)
            .Select(thread => thread.Task)
            .Where(task => task != null)
            .ToArray() as Task[];
    }

    public static void WaitSeconds(int seconds = 1)
    {
        const int defaultSeconds = 1;
        const int millisecondsPerSecond = 1000;

        var milliseconds = millisecondsPerSecond * (seconds <= 0
            ? defaultSeconds
            : seconds);

        Thread.Sleep(milliseconds);
    }

    public static Task EmptyAwait()
    {
        return Task.FromResult(0);
    }

    public static async Task WaitForCancellationToken(CancellationToken token)
    {
        try
        {
            await Task.Delay(Timeout.Infinite, token);
        }
        catch
        {
        }
    }
}
