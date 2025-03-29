namespace DogeServer.enums;

public static class EnumUtil
{
    public static string Value(Level enumValue)
    {
        return enumValue.ToString().ToLower();
    }

    public static Level ChildLevel(Level parent)
    {
        return parent switch
        {
            Level.Title => Level.Chapter,
            Level.Chapter => Level.Subchapter,
            Level.Subchapter => Level.Part,
            Level.Part => Level.Subpart,
            Level.Subpart => Level.Section,
            Level.Section => Level.Paragraph,
            Level.Paragraph => default,
            _ => default,
        };
    }
}
