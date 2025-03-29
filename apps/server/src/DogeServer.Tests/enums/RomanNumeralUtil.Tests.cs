using DogeServer.Tests.NUnit;
using DogeServer.Util;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace DogeServer.Tests.enums;

public class RomanNumeralUtilTests : BaseFixture
{
    [TestCase(null, null)]
    [TestCase(-1, null)]
    [TestCase(0, null)]
    [TestCase(4000, null)]
    [TestCase(-1, null)]
    [TestCase(1, "I")]
    [TestCase(4, "IV")]
    [TestCase(9, "IX")]
    [TestCase(40, "XL")]
    [TestCase(90, "XC")]
    [TestCase(400, "CD")]
    [TestCase(900, "CM")]
    [TestCase(1994, "MCMXCIV")]
    [TestCase(3999, "MMMCMXCIX")]
    public void IntToRoman(int? input, string? expected)
    {
        var actual = RomanNumeralUtil.Convert(input);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(null, null)]
    [TestCase("", null)]
    [TestCase("VX", null)]
    [TestCase("ABC", null)]
    [TestCase("MCMXCIVX", null)]
    [TestCase("I", 1)]
    [TestCase("IIII", 4)] //TODO: Invalid? 
    [TestCase("IX", 9)]
    [TestCase("XL", 40)]
    [TestCase("XC", 90)]
    [TestCase("CD", 400)]
    [TestCase("CM", 900)]
    [TestCase("MCMXCIV", 1994)]
    [TestCase("MMMCMXCIX", 3999)]
    public void RomanToInt(string? input, int? expected)
    {
        var actual = RomanNumeralUtil.Convert(input);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
