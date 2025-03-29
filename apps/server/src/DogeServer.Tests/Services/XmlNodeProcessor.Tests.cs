using DogeServer.enums;
using DogeServer.Models.DTO;
using DogeServer.Services.Seed;
using DogeServer.Tests.NUnit;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace DogeServer.Tests.Services;

public class XmlNodeProcessorTests : BaseFixture
{
    [TestCase(null, null)]
    [TestCase("", null)]
    [TestCase("Chapter", null)]
    [TestCase(" Chapter", null)]
    [TestCase("Chapter ", null)]
    [TestCase("Chapter I", "I")]
    [TestCase(" Chapter II", "II")]
    [TestCase("Chapter II ", "II")]
    [TestCase(" Chapter XIV [RESERVED]", "XIV")]
    [TestCase(" CHAPTER IX [RESERVED]", "IX")]
    [TestCase(" CHAPTER IV-U.S. IMMIGRATION", "IV")]
    public void ParseHeaderInToExpectedLabelLevel(string? input, string? expected)
    {
        var actual = ExpectedLabelUtil.ParseHeaderInToExpectedLabelLevel(input);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(null, null, null, null)] // error case
    [TestCase(null, "IV", "ABC", null)] // error case
    [TestCase(Level.Title, "IV", "ABC", "Title IV")]
    [TestCase(Level.Chapter, "IV", "ABC", "Chapter IV")]
    [TestCase(Level.Title, "IV", "ABC", "Title IV")]
    [TestCase(Level.Chapter, "IV", "ABC", "Chapter IV")]
    [TestCase(Level.Title, "14", "ABC", "Title XIV")]
    [TestCase(Level.Chapter, "14", "ABC", "Chapter XIV")]
    [TestCase(Level.Chapter, "14 14", "ABC", "Chapter XIV")] // invalid NUM
    [TestCase(Level.Chapter, "Chapter 14", "ABC", "Chapter XIV")] // invalid NUM
    [TestCase(Level.Chapter, null, "Chapter XXIV", "Chapter XXIV")] // ParseHeaderInToExpectedLabelLevel
    [TestCase(Level.Chapter, "", "Chapter XXIV", "Chapter XXIV")] // ParseHeaderInToExpectedLabelLevel
    [TestCase(Level.Chapter, "IX[RESERVED]", " CHAPTER IV-U.S. IMMIGRATION", "Chapter IX")]
    public void ExpectedLabel(Level? level, string? num, string? header, string? expected)
    {
        Div? input = new()
        {
            Num = num,
            Header = header
        };

        var actual = ExpectedLabelUtil.ParseExpectedLabel(level, input);
        if (expected == null)
        {
            Assert.That(actual, Is.Null);
        }
        else
        {
            var romanActual = actual?.RomanLabel;
            Assert.That(romanActual, Is.EqualTo(expected));
        }            
    }
}
