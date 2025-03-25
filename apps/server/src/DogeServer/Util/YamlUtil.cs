using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DogeServer.Util;

public static class YamlUtil
{
    public static void Serialize<T>(T obj, string? filename)
    {
        const string dirName = "export";
        
        filename ??= StringUtil.Random();
        filename += ".yml";

        string dir = Path.Combine(Directory.GetCurrentDirectory(), dirName);
        Directory.CreateDirectory(dir);

        string filePath = Path.Combine(dir, filename);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults)
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitEmptyCollections)
            .Build();

        var yaml = serializer.Serialize(obj);

        File.WriteAllText(filePath, yaml);
    }
}