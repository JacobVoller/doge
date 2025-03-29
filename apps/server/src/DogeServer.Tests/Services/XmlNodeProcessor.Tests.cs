using DogeServer.Services;
using NUnit.Framework;

using Assert = NUnit.Framework.Assert;

namespace DogeServer.Tests.Services;

public class XmlNodeProcessor_Tests
{
    [TestCase("", null)]
    [TestCase("Chapter", null)]
    [TestCase("Chapter I", "Chapter I")]
    [TestCase(" Chapter II ", "Chapter II")]
    [TestCase(" Chapter XIV [RESERVED]", "Chapter XIV")]
    public void ParseHeaderInToExpectedLabelLevel(string? input, string? expected)
    {
        var actual = XmlNodeProcessor.ParseHeaderInToExpectedLabelLevel(input);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
