using DogeServer.enums;
using DogeServer.Models.DTO;
using DogeServer.Util;

namespace DogeServer.Services.Seed;

public static class ExpectedLabelUtil
{
    public static DivLabel? ParseExpectedLabel(Level? level, Div? node)
    {
        if (level == null) return default;
        if (node == null) return default;

        var num = node?.Num?.Trim();
        if (string.IsNullOrEmpty(num) || num == "0")
        {
            num = ParseHeaderInToExpectedLabelLevel(node?.Header);
            return DivLabel.Factory(level, num);
        }

        var parts = num.Split(CharacterConsts.Space);
        num = (parts.Length == 1)
            ? parts[0]
            : (parts.Length > 1)
                ? parts[1]
                : null;

        return (string.IsNullOrEmpty(num))
            ? default
            : DivLabel.Factory(level, num);
    }

    public static string? ParseHeaderInToExpectedLabelLevel(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return default;

        input = input.Trim();
        var splitIndex = input.IndexOf(CharacterConsts.Space);
        if (splitIndex > 0)
        {
            input = input[(splitIndex + 1)..].Trim();
        }

        var dashIndex = input.IndexOf(CharacterConsts.Dash);
        splitIndex = input.IndexOf(CharacterConsts.Space);
        splitIndex = (splitIndex >= 0)
            ? (dashIndex >= 0)
                ? Math.Min(splitIndex, dashIndex)
                : splitIndex
            : dashIndex;

        var roman = (splitIndex > 0)
            ? input[..splitIndex].Trim()
            : input.Trim();

        if (string.IsNullOrWhiteSpace(roman)) return default;
        return RomanNumeralUtil.IsValid(roman)
            ? roman
            : default;
    }
}
