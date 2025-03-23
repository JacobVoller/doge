namespace DogeServer.Util;

public class ThreadUtil
{
    public CancellationTokenSource CancelTrigger { get; set; } = new();
    public Task? Task { get; set; }

    public TaskStatus? Status
    {
        get
        {
            return Task?.Status ?? null;
        }
    }

    public void Cancel()
    {
        CancelTrigger.Cancel();
    }

    public async Task GetAwaiter()
    {
        if (Task == null) return;

        await Task;
    }
}
