using DogeServer.Models.Entities;

namespace DogeServer.Data.Managers;

public class OutlineManager(Func<DatabaseContext> dbConnectCallback)
    : BaseManager<Outline>(dbConnectCallback)
{
}
