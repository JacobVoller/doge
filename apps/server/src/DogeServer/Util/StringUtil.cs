using System.Text.RegularExpressions;

namespace DogeServer.Util
{
    public static class StringUtil
    {
        private static readonly string invalidInputReturnType = string.Empty;

        public static string RemoveFirstChar(string? input)
        {
            return string.IsNullOrEmpty(input)
                ? invalidInputReturnType
                : input.Substring(1);
        }

        public static string? RemoveLastChar(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            const int charsToRemove = 1;
            var endIndex = input.Length - charsToRemove;
            return input[..endIndex];
        }

        public static string EscapeBackslashes(string? input)
        {
            return string.IsNullOrWhiteSpace(input)
                ? invalidInputReturnType
                : input.Replace("\\\"", "\"");
        }

        public static string Consolidate(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return invalidInputReturnType;

            input = input
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty);

            return
                Regex.Replace(input, @"\s+", " ")
                .Trim();
        }

        public static bool IsEquivalent(string? a, string? b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }

        public static string Random(int? requestedLength = null)
        {
            const int defaultLength = 10;
            var length = (requestedLength == null || requestedLength <= 0)
                ? defaultLength
                : (int)requestedLength;

            const string possibleCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var possibleCharactersLength = possibleCharacters.Length;
            var random = new Random();
            var randomCharacters = new char[length];

            for (var index = 0; index < length; index++)
            {
                randomCharacters[index] = possibleCharacters[random.Next(possibleCharactersLength)];
            }

            return new string(randomCharacters);
        }

        public static string? JoinWithComma(IEnumerable<string?>? strings)
        {
            if (strings == null)
                return null;

            var toJoin = strings.Where(str => !string.IsNullOrWhiteSpace(str));
            return string.Join(", ", toJoin);
        }

        public static string? JoinWithNewLine(IEnumerable<string?>? strings, bool prefixLineNumbers = true)
        {
            if (strings == null)
                return null;

            var toJoin = strings
                .Where(str => !string.IsNullOrWhiteSpace(str));

            if (prefixLineNumbers)
            {
                var doubleDigits = strings.Count() > 9;
                toJoin = toJoin.Select((str, index) =>
                {
                    var lineNumber = index + 1;
                    var prefix = doubleDigits
                        ? lineNumber.ToString("D2")
                        : lineNumber.ToString();

                    return $"{prefix}. {str}";
                });
            }

            return string.Join("\n", toJoin);
        }

    }
}
