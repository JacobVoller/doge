using DogeServer.Models.Entities;
using DogeServer.Util;
using Microsoft.EntityFrameworkCore;

namespace DogeServer.Data;

public abstract class BaseManager<T>(Func<DatabaseContext> dbConnectCallback) where T : Entity
{
    protected readonly Func<DatabaseContext> ConnectToDatabase = dbConnectCallback;

    public async Task<K> ExecuteDatabaseQuery<K>(Func<DatabaseContext, Task<K>> databaseOperations)
    {
        using var databaseContext = ConnectToDatabase();
        K result = await databaseOperations(databaseContext);
        await databaseContext.SaveChangesAsync();

        return result;
    }

    public async Task<T?> GetFromID(Guid? id)
    {
        T? invalid = null;

        if (id == null)
            return invalid;

        return await ExecuteDatabaseQuery(async db =>
        {
            var table = db.GetTable<T>();
            if (table == null)
                return invalid;

            return await table
                .Where(x => x != null
                            && x.Deleted == null)
                .FirstOrDefaultAsync(x => x.ID == id);
        });
    }

    public async Task<bool> DeleteById(Guid? id)
    {
        const bool success = true;

        if (id == null)
            return !success;

        var entity = await GetFromID(id);
        if (entity == null)
            return !success;

        var instance = ReflectionUtil.CreateInstance(typeof(T));
        if (instance == null)
            return !success;

        entity.Deleted = DateTime.UtcNow;
        var guid = await Update(entity);

        return guid != null;
    }

    public async Task<Guid?> Create(T entity)
    {
        Guid? createdEntityID = null;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        await ExecuteDatabaseQuery(async db =>
        {
            var table = db.GetTable<T>();
            table?.Add(entity);

            return createdEntityID;
        });
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        return entity.ID;
    }

    public async Task<Guid?> Update(T? entity)
    {
        if (entity == null)
            return null;

        return await ExecuteDatabaseQuery(async db =>
        {
            var table = db.GetTable<T>();
            if (table == null) return null;

            var existingEntry = await table
                .Where(x => x.Deleted == null)
                .FirstOrDefaultAsync(x => x.ID == entity.ID);

            if (existingEntry == null)
                return null;

            EntityUtil.Combine(existingEntry, entity);
            return existingEntry.ID;
        });
    }

    public virtual async Task<Guid?> CreateOrUpdate(T? entity)
    {
        if (entity == null)
            return null;

        var exists = await GetFromID(entity.ID);
        if (exists == null)
        {
            return await Create(entity);
        }
        else
        {
            return await Update(entity);
        }
    }

    public async Task<List<T>> GetAll()
    {
        var array = await ExecuteDatabaseQuery(async db =>
        {
            if (db == null)
                return [];

            var table = db.GetTable<T>();
            if (table == null)
                return [];

            return await table
                .Where(entity => entity != null
                            && entity.Deleted == null)
                .ToArrayAsync();
        });

        return [.. array];
    }

    protected async Task<K?> Get<K>(Func<IQueryable<T?>, Task<K>> searchFunction) where K : class?
    {
        if (searchFunction == null)
            return null;

        return await ExecuteDatabaseQuery(async db =>
        {
            var table = db.GetTable<T>();
            if (table == null)
                return null;

            var rows = table.Where(x => x.Deleted == null);
            if (rows == null)
                return null;

            return await searchFunction(rows);
        });
    }
}
