using DogeServer.Data;
using DogeServer.enums;
using DogeServer.Models.DTO;
using DogeServer.Models.Entities;
using DogeServer.Util;
using System;
using System.Xml.Linq;

namespace DogeServer.Services;

public class XmlNodeProcessor
{
    public static XmlNodeProcessor Factory(DataLake dataLake)
    {
        return new XmlNodeProcessor(dataLake);  
    }

    protected DataLake DataLake { get; set; }

    protected bool seeded = false;
    protected List<Outline>? _chapters;

    public XmlNodeProcessor(DataLake dataLake) => DataLake = dataLake;

    protected async Task Seed()
    {
        await DataLake.Outline.GetChapters();
    }

    public async Task Volume(Div? node, Level? l = null, Guid? parentId = null)
    {
        if (!seeded) await Seed();
        
        if (node == null) return;

        var level = l ?? Level.Title;
        var expectedLabelLevel = ExpectedLabel(level, node.Num);
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
            tasks.AddRange(node.Chapter.Select(node => Volume(
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

    public string? ExpectedLabel(Level? level, string? num)
    {
        if (string.IsNullOrEmpty(num)) return default;
        if (level == null) return default;

        return level switch
        {
            Level.Title => $"Title {num}",
            Level.Chapter => $"Chapter {num}",
            _ => default,
        };
    }









}
