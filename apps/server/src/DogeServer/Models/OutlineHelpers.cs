
namespace DogeServer.Models.Entities;

public partial class Outline
{

    public Tuple<string?, string?> GetRequestComponents()
    {
        var title = Number?.ToString();
        var date = LastUpdated
                   ??LastIssued
                   ?? LastAmended
                   ?? null;

        return Tuple.Create(date, title);
    }
}