using DogeServer.Config.Models;
using DogeServer.Util;

namespace DogeServer.Config;

public static class AppConfiguration
{
    public static DatabaseConfig Database { get; private set; }
    public static EcfrConfig eCFR { get; private set; }

    public static void Init()
    {
        var config = BuildConfig();

        Database = Configure<DatabaseConfig>(config, "database");
        eCFR = Configure<EcfrConfig>(config, "ecfr");
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

    private static T Configure<T>(IConfiguration config, string? section) where T : class
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