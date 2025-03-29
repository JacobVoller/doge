using DogeServer.Data;
using DogeServer.enums;
using DogeServer.Models.DTO;
using DogeServer.Services.Seed;
using DogeServer.Util;

namespace DogeServer.Services;

public partial class XmlNodeProcessor(DataLake dataLake)
{
    public static XmlNodeProcessor Factory(DataLake dataLake)
    {
        return new XmlNodeProcessor(dataLake);  
    }

    protected DataLake DataLake { get; set; } = dataLake;

    public async Task Volume(Div? node, Level? l = null, Guid? parentId = null)
    {
        if (!seeded)
        {
            await Seed();
        }
        
        if (node == null) return;

        var level = l ?? Level.Title;
        var divLabel = ExpectedLabelUtil.ParseExpectedLabel(level, node);
        var intOutline = await DataLake.Outline.GetOutlineByLevelAndLabel(level, divLabel?.RomanLabel, parentId);
        intOutline ??= await DataLake.Outline.GetOutlineByLevelAndLabel(level, divLabel?.IntLabel, parentId);

        if (intOutline == null || intOutline.ID == null)
        {
            var lvl = level.ToString().ToLower();
            var msg = $"XmlNodeProcessor Failure: [lvl={lvl}] {divLabel?.RomanLabel} / {divLabel?.IntLabel}";
            DebugUtil.Log(msg);

            return;
        }

        //TODO: Save

        var guid = (Guid)intOutline.ID;
        var tasks = new List<Task>();

        if (node.Chapter != null)
        {
            tasks.AddRange(
                node.Chapter.Select(
                    node => Volume(
                        node,
                        Level.Chapter,
                        guid)));
        }

        Task.WaitAll(tasks);
    }
}
