using DogeServer.Data;
using DogeServer.enums;
using DogeServer.Models.DTO;
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

    protected string? ExpectedLabel(Level? level, Div? node)
    {
        if (level == null) return default;
        if (node == null) return default;

        var num = node.Num;

        if (num == null || num == "0")
        {
            return ParseHeaderInToExpectedLabelLevel(node.Header);
        }

        //var num = node.Num == null
        //    ? 0
        //    : int.Parse(node.Num);
        //if (num < 1) return default;

        //var roman = RomanNumeralUtil.Convert(num);

        return level switch
        {
            Level.Title => $"Title {num}",
            Level.Chapter => $"Chapter {num}",
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
}
