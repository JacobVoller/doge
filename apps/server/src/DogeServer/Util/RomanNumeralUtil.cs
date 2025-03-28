using System.Text;

namespace DogeServer.Util;

public static class RomanNumeralUtil
{
    private static Dictionary<int, string> Map = new Dictionary<int, string>
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

    public static string? Convert(int input)
    {
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
}
