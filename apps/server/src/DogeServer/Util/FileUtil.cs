using DogeServer.Config;

namespace DogeServer.Util;

public static class FileUtil
{
    public static string GetDirectory()
    {
        return Directory.GetCurrentDirectory();
    }

    public static string GetDirectory(string? dir)
    {
        return (string.IsNullOrEmpty(dir))
            ? GetDirectory()
            : Path.Combine(GetDirectory(), dir);
    }

    public static string CreateDirectory(string? dir)
    {
        dir ??= AppConfiguration.ExportDirectory;

        var dirPath = GetDirectory(dir);
        Directory.CreateDirectory(dirPath);

        return dirPath;
    }

    public static string? CreateFilePath(string? dir, string? file)
    {
        if (file == null) return default;
        if (dir == null) return default;
        
        return Path.Combine(dir, file);
    }
    
    public static string? PrepareForFileCreate(string? dir, string? file)
    {
        dir = CreateDirectory(dir);

        var filePath = CreateFilePath(dir, file);
        if (filePath == null) return default;
        if (!File.Exists(filePath)) return filePath;
        
        File.Delete(filePath);

        return filePath;
    }
    
    public static void Create(string data, string? dir, string? filename, string? extension)
    {
        dir ??= AppConfiguration.ExportDirectory;
        filename ??= StringUtil.Random();
        extension ??= "txt";
        
        var file = $"{filename}.{extension}";
        var filePath = PrepareForFileCreate(dir, file);
        if (string.IsNullOrEmpty(filePath)) return;

        File.WriteAllText(filePath, data);
    }
}
