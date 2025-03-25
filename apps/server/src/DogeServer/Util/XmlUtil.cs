using System.Xml.Serialization;

namespace DogeServer.Util;

public static class XmlUtil
{
    public static T? DeSerialize<T>(Stream? xmlStream)
    {
        if (xmlStream == null) return default;

        var serializer = new XmlSerializer(typeof(T));
        var obj = serializer.Deserialize(xmlStream);

        return (obj == null)
            ? default
            : (T)obj;
    }
}
