using System.Text;
using System.Text.RegularExpressions;

namespace DogeServer.Util;

public static class RomanNumeralUtil
{
    private static readonly Dictionary<int, string> Map = new()
    {
        {1000, "M"},
        {900, "CM"},
        {500, "D"},
        {400, "CD"},
        {100, "C"},
        {90, "XC"},
        {50, "L"},
        {40, "XL"},
        {10, "X"},
        {9, "IX"},
        {5, "V"},
        {4, "IV"},
        {1, "I"}
    };

    public static string? Convert(int? input)
    {
        if (input == null) return default;
        if (input < 1) return default;
        if (input > 3999) return default;

        var result = new StringBuilder();

        foreach (var (value, numeral) in Map)
        {
            while (input >= value)
            {
                result.Append(numeral);
                input -= value;
            }
        }

        return result.ToString();
    }

    public static int? Convert(string? roman)
    {
        if (string.IsNullOrWhiteSpace(roman)) return null;

        roman = roman.ToUpperInvariant();
        int index = 0;
        int total = 0;

        foreach (var (value, numeral) in Map)
        {
            while (roman.AsSpan(index).StartsWith(numeral))
            {
                total += value;
                index += numeral.Length;
            }
        }

        return index == roman.Length
            ? total
            : null;
    }

    public static bool IsValid(string? roman)
    {
        try
        {
            var converted = Convert(roman);
            return converted != null && converted > 0;
        }
        catch
        {
            return false;
        }
    }
}
