using DogeServer.enums;
using DogeServer.Util;

namespace DogeServer.Services.Seed;

public class DivLabel
{
    public static DivLabel? Factory(Level? level, string? input)
    {
        if (level == null
            || string.IsNullOrWhiteSpace(input)) return default;

        var romanNumber = string.Empty;
        var inputIsInt = int.TryParse(input, out int parsedInt);
        int? intNumber = inputIsInt
            ? parsedInt
            : RomanNumeralUtil.Convert(input);

        romanNumber = inputIsInt
            ? RomanNumeralUtil.Convert(parsedInt)
            : input;

        if (intNumber == null
            || intNumber <= 0
            || string.IsNullOrWhiteSpace(romanNumber)) return default;

        return new DivLabel((Level)level, (int)intNumber, romanNumber);
    }

    public readonly string IntLabel;
    public readonly string RomanLabel;

    protected DivLabel(Level level, int number, string roman)
    {
        RomanLabel = GenerateLabel(level, roman);
        IntLabel = GenerateLabel(level, number.ToString());
    }

    protected string GenerateLabel(Level level, string num)
    {
        return level switch
        {
            Level.Title => $"Title {num}",
            Level.Chapter => $"Chapter {num}",
            _ => string.Empty,
        };
    }
}
