using DogeServer.Models.Entities;

namespace DogeServer.Data.Managers;

public class TitleManager(Func<DatabaseContext> dbConnectCallback)
    : BaseManager<Title>(dbConnectCallback)
{
}
