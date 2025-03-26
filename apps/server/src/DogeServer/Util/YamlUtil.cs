using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DogeServer.Util;

public static class YamlUtil
{
    public static string Serialize<T>(T obj)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults)
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitEmptyCollections)
            .Build();

        return serializer.Serialize(obj);
    }

    public static void CreateFile<T>(T obj, string? filename)
    {
        const string dir = "export";
        const string ext = "yml";
        
        filename ??= StringUtil.Random();

        var yaml = Serialize(obj);

        FileUtil.Create(yaml, dir, filename, ext);
    }
}