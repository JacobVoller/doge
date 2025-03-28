using DogeServer.enums;
using DogeServer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogeServer.Data.Managers;

public class OutlineManager(Func<DatabaseContext> dbConnectCallback)
    : BaseManager<Outline>(dbConnectCallback)
{
    public async Task<List<Outline>> GetTitles()
    {
        return await GetOutlinesByHierarchy(Level.Title);
    }

    public async Task<List<Outline>> GetChapters()
    {
        return await GetOutlinesByHierarchy(Level.Chapter);
    }

    protected async Task<List<Outline>> GetOutlinesByHierarchy(Level level)
    {
        var typeIdentifier = EnumUtil.Value(level);
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
                    && outline.Type.Contains(typeIdentifier, StringComparison.CurrentCultureIgnoreCase))
                .ToArrayAsync();
        });

        return [.. array];
    }

    public async Task<Outline?> GetOutlineByLevelAndLabel(Level level, string? keyword, Guid? parentId = null)
    {
        if (string.IsNullOrEmpty(keyword)) return default;

        var typeIdentifier = EnumUtil.Value(level);
        keyword = keyword.ToLower().Trim();

        var array = await ExecuteDatabaseQuery(async db =>
        {
            if (db == null)
                return [];

            var table = db.GetTable<Outline>();
            if (table == null)
                return [];

            var results = table
                .Where(outline => outline.Removed != true)
                .Where(outline => outline.Deleted == null)
                .Where(outline =>
                    outline.Type != null
                    && outline.Type.Equals(typeIdentifier, StringComparison.CurrentCultureIgnoreCase));

            if (parentId != null)
            {
                results = results.Where(outline =>
                    outline.ParentID == parentId);
            }


            if (level == Level.Title || level == Level.Chapter)
            {
                results = results.Where(outline =>
                    outline.LabelLevel != null
                    && outline.LabelLevel.Equals(keyword, StringComparison.CurrentCultureIgnoreCase));
            }

            //if (level == Level.Chapter)
            //{
            //    results = results.Where(outline =>
            //        outline.LabelLevel != null
            //        && outline.LabelLevel.Equals(keyword, StringComparison.CurrentCultureIgnoreCase));
            //}

            return await results.ToArrayAsync();
        });

        if (array == null) return default;
        if (array.Length == 0) return default;

        return array.FirstOrDefault();
    }

}
