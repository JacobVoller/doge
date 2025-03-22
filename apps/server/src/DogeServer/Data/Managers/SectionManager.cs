using DogeServer.Models.Entities;

namespace DogeServer.Data.Managers;

public class SectionManager(Func<DatabaseContext> dbConnectCallback)
    : BaseManager<Section>(dbConnectCallback)
{
}
