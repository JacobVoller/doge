using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace DogeServer.Util;

public static class XmlUtil
{
    public static T? DeSerialize<T>(Stream? xmlStream)
    {
        string? xml = null;
        
        try
        {
            xml = CleanXml(xmlStream);
            if (xml == null) return default;

            var bytes = Encoding.UTF8.GetBytes(xml);
            using var cleanStream = new MemoryStream(bytes);

            var serializer = new XmlSerializer(typeof(T));
            var obj = serializer.Deserialize(cleanStream);

            return (obj == null)
                ? default
                : (T)obj;
        }
        catch (Exception exception)
        {
            var message = $"{exception.Message}: ";
            var lineNo = ExtractLineNumberFromExceptionMessage(exception.Message);

            if (lineNo != null)
            {
                message += StringUtil.GetLine(xml, lineNo-1);
                message += StringUtil.GetLine(xml, lineNo);
                message += StringUtil.GetLine(xml, lineNo+1);
            }

            throw new Exception(message);
        }
    }

    private static string? CleanXml(Stream? xmlStream)
    {
        if (xmlStream == null) return default;

        using var reader = new StreamReader(xmlStream);
        var rawXml = reader.ReadToEnd();

        if (rawXml == null) return default;

        rawXml = RemoveOpenAndCloseTags(rawXml);
        rawXml = RemoveCloseTags(rawXml);
        rawXml = ReplaceTagsWithInlineTag(rawXml);
        rawXml = ReplaceImgTags(rawXml);

        return rawXml;
    }

    public static int? ExtractLineNumberFromExceptionMessage(string? input)
    {
        if (input == null) return default;
        
        var match = Regex.Match(input, @"\((\d+), \d+\)\.");
        return match.Success
            ? int.Parse(match.Groups[1].Value)
            : default;
    }

    private static string? RemoveOpenAndCloseTags(string? rawXml)
    {
        if (string.IsNullOrEmpty(rawXml)) return default;
        
        var tagsToRemove = new string[]
        {
            "E",
            "I",
            "SU",
            "FR",
            "B",
            "AC",
            "HED1",
            "SECAUTH"
        };

        foreach (var tag in tagsToRemove)
        {
            rawXml = rawXml.Replace($"<{tag}>", string.Empty);
            rawXml = Regex.Replace(rawXml, @$"<{tag}\s[A-Za-z0-9]+=""[A-Za-z0-9]+"">", string.Empty);
            rawXml = Regex.Replace(rawXml, @$"<{tag}\s[A-Za-z0-9]+=""[A-Za-z0-9]+""/>", string.Empty);
            rawXml = rawXml.Replace($"</{tag}>", string.Empty);
            rawXml = rawXml.Replace($"<{tag}/>", string.Empty);
        }

        return rawXml;
    }

    private static string? RemoveCloseTags(string? rawXml)
    {
        if (string.IsNullOrEmpty(rawXml)) return default;

        var tagsToRemove = new string[]
        {
            "FTREF"
        };

        foreach (var tag in tagsToRemove)
        {
            rawXml = rawXml.Replace($"<{tag}/>", string.Empty);
        }

        return rawXml;
    }

    private static string? ReplaceTagsWithInlineTag(string? rawXml)
    {
        if (string.IsNullOrEmpty(rawXml)) return default;

        var tagsToRemove = new string[]
        {
            "HD1",
            "HD2",
            "HD3",
            "HD4",
            "HD5",
            "FP-DASH",
            "FP",
            "FP-1",
            "FP-2",
            "FRP",
            "HD1",
            "HD2",
            "HD3",
            "HD4",
            "HD5"
        };

        foreach (var tag in tagsToRemove)
        {
            rawXml = rawXml.Replace($"<{tag}>", $"[{tag}]");
            rawXml = rawXml.Replace($"<{tag}/>", $"[{tag}/]");
            rawXml = rawXml.Replace($"</{tag}>", $"[/{tag}]");
        }

        return rawXml;
    }

    private static string? ReplaceImgTags(string? rawXml)
    {
        if (string.IsNullOrEmpty(rawXml)) return default;

        const string pattern = @"<img\b[^>]*\/>";

        return Regex.Replace(rawXml, pattern, m =>
        {
            string tag = m.Value;
            return "[" + tag.Substring(1, tag.Length - 2) + "]";
        });
    }
}
