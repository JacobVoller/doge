using System.Runtime.ExceptionServices;

namespace DogeServer.Util;

public static class ExceptionUtil
{
    public static void Rethrow(Exception exception)
    {
        ExceptionDispatchInfo.Capture(exception).Throw();
    }
}
