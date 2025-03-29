using DogeServer.Models.Entities;

namespace DogeServer.Services;

public partial class XmlNodeProcessor
{
    // ---DEBUG-----------------------------------------
    protected bool seeded = false;
    protected Dictionary<string, List<Outline>> _all = [];
    protected Dictionary<string, List<Outline>> _chapters = [];
    // ---DEBUG-----------------------------------------

    protected async Task Seed()
    {
        var temp = await DataLake.Outline.GetAll();
        var q = new Queue<Outline>(temp);
        
        while (q.Count > 0)
        {
            var t = q.Dequeue();
            ProcessByLevel(t);
        }

        ReprocessChapters();

        seeded = true;
    }

    private void ProcessByLevel(Outline outline)
    {
        var type = outline.Type ?? "X";

        if (!_all.ContainsKey(type))
        {
            _all.Add(type, []);
        }

        _all[type].Add(outline);
    }

    private void ReprocessChapters()
    {
        var chptrs = _all["chapter"];
        if (chptrs == null) return;

        var chapters = chptrs
            .OrderBy(o => o.ParentID)
            .ToList();

        foreach (var c in chapters)
        {
            var parent = c?.ParentID?.ToString() ?? "X";
            if (!_chapters.ContainsKey(parent))
            {
                _chapters.Add(parent, []);
            }

            _chapters[parent].Add(c);
        }
    }
}
