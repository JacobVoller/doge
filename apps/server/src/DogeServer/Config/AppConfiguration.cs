using DogeServer.Config.Models;
using DogeServer.Util;

namespace DogeServer.Config;

public static class AppConfiguration
{
    public static DatabaseConfig Database { get; private set; } = new();
    public static EcfrConfig eCFR { get; private set; } = new();
    public static StartupConfig Startup { get; private set; } = new();
    public static string ExportDirectory { get; private set; } = string.Empty;

    public static void Init()
    {
        var config = BuildConfig();

        Database = Configure<DatabaseConfig>(config, "database") ?? new();
        eCFR = Configure<EcfrConfig>(config, "ecfr") ?? new();
        Startup = Configure<StartupConfig>(config, "startup") ?? new();
        ExportDirectory = Configure<string>(config, "exportDirectory") ?? string.Empty;
    }

    private static IConfiguration BuildConfig()
    {
        const string appSettingsFile = "appsettings.json";
        var currentDirectory = Directory.GetCurrentDirectory();
        var builder = new ConfigurationBuilder()
            .SetBasePath(currentDirectory)
            .AddJsonFile(appSettingsFile, optional: true, reloadOnChange: true);

        return builder.Build();
    }

    private static T? Configure<T>(IConfiguration config, string? section) where T : class
    {
        if (config == null)
            return (ReflectionUtil.CreateInstance<T>() as T);

        if (!string.IsNullOrWhiteSpace(section))
        {
            config = config.GetSection(section);
        }

        return config.Get<T>()
            ?? throw new Exception($"Failed to parse {section} from appsettings.json");
    }

}