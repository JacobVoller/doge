using System.Reflection;

namespace DogeServer.Util;

public static class ReflectionUtil
{
    public static object CreateInstance<T>()
    {
        var type = typeof(T);
        
        var instance = Activator.CreateInstance(type);
        if (instance != null)
            return instance;

        throw new Exception($"ReflectionUtil.CreateInstance failed in create an instance of {type.Name}.");
    }

    public static object? CreateInstance(Type type)
    {
        return Activator.CreateInstance(type);
    }

    public static bool GetBoolValue(Type type, string fieldName)
    {
        var fieldInfo = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        if (fieldInfo == null)
            throw new ArgumentException($"Field '{fieldName}' in type '{type.FullName}'.");

        if (fieldInfo.FieldType != typeof(bool))
            throw new ArgumentException($"Field '{fieldName}' is not a public const bool in type '{type.FullName}'.");

        if (!fieldInfo.IsLiteral)
            throw new ArgumentException($"Field '{fieldName}' is not literal in type '{type.FullName}'.");

        var value = fieldInfo.GetValue(null);
        if (value == null)
            throw new ArgumentException($"Field '{fieldName}' type '{type.FullName}' is null.");

        return (bool)value;
    }

    public static T? DeepClone<T>(T obj)
    {
        var json = JsonUtil.Serialize(obj);
        return JsonUtil.DeSerialize<T>(json);
    }
}
