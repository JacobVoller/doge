using Newtonsoft.Json;

namespace DogeServer.Util
{
    public static class JsonUtil
    {
        private static readonly JsonSerializerSettings Settings = new()
        {
            Formatting = Formatting.Indented
        };

        public static string? Serialize(object? o)
        {
            return (o == null)
                ? null
                : JsonConvert.SerializeObject(o, Settings);
        }

        public static T? DeSerialize<T>(string? json)
        {
            T? invalid = default;

            if (string.IsNullOrWhiteSpace(json))
                return invalid;

            if (json.Contains("\\\"")) // encoded
            {
                json = StringUtil.RemoveFirstChar(json);
                json = StringUtil.RemoveLastChar(json);
                json = StringUtil.EscapeBackslashes(json);
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string? TrimQuotes(string? input)
        {
            if (input == null) return input;

            var startsAndEndsWithQuote = input.Length >= 2 && input[0] == '"' && input[^1] == '"';
            return startsAndEndsWithQuote
                ? input.Substring(1, input.Length - 2)
                : input;
        }
    }
}
