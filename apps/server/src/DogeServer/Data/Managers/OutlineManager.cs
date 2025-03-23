using DogeServer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogeServer.Data.Managers;

public class OutlineManager(Func<DatabaseContext> dbConnectCallback)
    : BaseManager<Outline>(dbConnectCallback)
{
    protected const string TitleIdentifier = "title";
    protected const string ChapterIdentifier = "chapter";
    protected const string SubchapterIdentifier = "subchapter";
    protected const string PartIdentifier = "part";
    protected const string SectionIdentifier = "section";

    public async Task<List<Outline>> GetTitles()
    {
        return await GetOutlinesByHierarchy(TitleIdentifier);
    }

    public async Task<List<Outline>> GetChapters()
    {
        return await GetOutlinesByHierarchy(ChapterIdentifier);
    }

    protected async Task<List<Outline>> GetOutlinesByHierarchy(string typeIdentifier)
    {
        var array = await ExecuteDatabaseQuery(async db =>
        {
            if (db == null)
                return [];

            var table = db.GetTable<Outline>();
            if (table == null)
                return [];

            return await table
                .Where(outline => outline.Removed != true)
                .Where(outline => outline.Deleted == null)
                .Where(outline =>
                    outline.Type != null
                    && outline.Type.Equals(typeIdentifier, StringComparison.CurrentCultureIgnoreCase))
                .ToArrayAsync();
        });

        return [.. array];
    }




}
