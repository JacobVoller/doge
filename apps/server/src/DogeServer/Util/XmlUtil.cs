using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using YamlDotNet.Core.Tokens;

namespace DogeServer.Util;

public static class XmlUtil
{
    public static T? DeSerialize<T>(Stream? xmlStream)
    {
        var xml = CleanXml(xmlStream);
        if (xml == null) return default;

        var bytes = Encoding.UTF8.GetBytes(xml);
        using var cleanStream = new MemoryStream(bytes);

        var serializer = new XmlSerializer(typeof(T));
        var obj = serializer.Deserialize(cleanStream);

        return (obj == null)
            ? default
            : (T)obj;
    }

    private static string? CleanXml(Stream? xmlStream)
    {
        if (xmlStream == null) return default;

        using var reader = new StreamReader(xmlStream);
        var rawXml = reader.ReadToEnd();

        if (rawXml == null) return default;

        var tags = new string[]
        {
            "E",
            "I",
            "SU",
            "FR",
            "B",
            "AC"
        };

        foreach (var tag in tags)
        {
            rawXml = rawXml.Replace($"<{tag}>", string.Empty);
            rawXml = Regex.Replace(rawXml, @$"<{tag}\s[A-Za-z0-9]+=""[A-Za-z0-9]+"">", string.Empty);
            rawXml = Regex.Replace(rawXml, @$"<{tag}\s[A-Za-z0-9]+=""[A-Za-z0-9]+""/>", string.Empty);
            rawXml = rawXml.Replace($"</{tag}>", string.Empty);
        }

        rawXml = rawXml.Replace($"<FTREF/>", string.Empty);

        return rawXml;
    }
}
