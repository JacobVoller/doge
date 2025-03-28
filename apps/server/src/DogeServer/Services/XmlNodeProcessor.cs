using DogeServer.Data;
using DogeServer.enums;
using DogeServer.Models.DTO;
using DogeServer.Models.Entities;
using DogeServer.Util;

namespace DogeServer.Services;

public class XmlNodeProcessor(DataLake dataLake)
{
    public static XmlNodeProcessor Factory(DataLake dataLake)
    {
        return new XmlNodeProcessor(dataLake);  
    }

    protected DataLake DataLake { get; set; } = dataLake;

    // ---DEBUG-----------------------------------------
    protected bool seeded = false;
    protected Dictionary<string, List<Outline>>? _all;
    // ---DEBUG-----------------------------------------

    protected async Task Seed()
    {
        _all = [];
        var temp = await DataLake.Outline.GetAll();
        foreach (var t in temp)
        {
            var type = t.Type ?? "X";
            if (!_all.ContainsKey(type))
            {
                _all.Add(type, []);
            }

            _all[type].Add(t);
        }

        seeded = true;
    }

    public async Task Volume(Div? node, Level? l = null, Guid? parentId = null)
    {
        if (!seeded)
        {
            await Seed();
        }
        
        if (node == null) return;

        var level = l ?? Level.Title;
        var expectedLabelLevel = ExpectedLabel(level, node);
        var intOutline = await DataLake.Outline.GetOutlineByLevelAndLabel(level, expectedLabelLevel, parentId);

        if (intOutline == null || intOutline.ID == null)
        {
            var lvl = level.ToString().ToLower();
            var msg = $"XmlNodeProcessor Failure: [lvl={lvl}] {expectedLabelLevel}";
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

    //public async Task Chapter(Div? node, Guid parentId)
    //{
    //    if (node == null) return;

    //    var level = Level.Chapter;
    //    var expectedLabelLevel = ExpectedLabel(level, node.Num);
    //    var saved = await DataLake.Outline.GetOutlineByLevelAndLabel(level, expectedLabelLevel, parentId);

    //    var debugger = 1;
    //}

    protected string? ExpectedLabel(Level? level, Div? node)
    {
        if (level == null) return default;
        if (node == null) return default;

        var num = node.Num == null
            ? 0
            : int.Parse(node.Num);
        if (num < 1) return default;

        var roman = RomanNumeralUtil.Convert(num);

        return level switch
        {
            Level.Title => $"Title {num}",
            Level.Chapter => $"Chapter {roman}",
            _ => default,
        };
    }

    protected Level ChildLevel(Level parent)
    {
        return parent switch
        {
            Level.Title => Level.Chapter,
            Level.Chapter => Level.Subchapter,
            Level.Subchapter => Level.Part,
            Level.Part => Level.Subpart,
            Level.Subpart => Level.Section,
            Level.Section => Level.Paragraph,
            Level.Paragraph => default,
            _ => default,
        };
    }

    protected static Dictionary<int, Outline>? OutlineListToMap(List<Outline>? list)
    {
        if (list == null) return default;
        if (list.Count == 0) return default;
        
        return list
            .Where(o => o != null)
            .Where(o => o.Number != null)
            .ToDictionary(
                o => (int)o.Number,
                o => o);
    }

}
