using DogeServer.enums;
using DogeServer.Models.Entities;
using System.Text;
using System.Text.RegularExpressions;

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

    public static string? ParseHeaderInToExpectedLabelLevel(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return default;

        input = input.Trim();
        string title = string.Empty;
        var splitIndex = input.IndexOf(CharacterConsts.Space);
        if (splitIndex > 0)
        {
            title = input[..splitIndex].Trim();
            input = input[(splitIndex + 1)..].Trim();
        }

        var dashIndex = input.IndexOf(CharacterConsts.Dash);
        splitIndex = input.IndexOf(CharacterConsts.Space);
        splitIndex = (splitIndex >= 0)
            ? (dashIndex >= 0)
                ? Math.Min(splitIndex, dashIndex)
                : splitIndex
            : dashIndex;

        var roman = (splitIndex > 0)
            ? input[..splitIndex].Trim()
            : input.Trim();

        if (string.IsNullOrWhiteSpace(roman)) return default;
        if (title.Length < 2) return default;

        return new StringBuilder()
            .Append(char.ToUpper(title[0]))
            .Append(title[1..].ToLower())
            .Append(' ')
            .Append(roman.ToUpper())
            .ToString();
    }
}
