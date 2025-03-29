﻿using DogeServer.Services;
using DogeServer.Tests.NUnit;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace DogeServer.Tests.Services;

public class XmlNodeProcessorTests : BaseFixture
{
    [TestCase("", null)]
    [TestCase("Chapter", null)]
    [TestCase(" Chapter", null)]
    [TestCase("Chapter ", null)]
    [TestCase("Chapter I", "Chapter I")]
    [TestCase(" Chapter II", "Chapter II")]
    [TestCase("Chapter II ", "Chapter II")]
    [TestCase(" Chapter XIV [RESERVED]", "Chapter XIV")]
    [TestCase(" CHAPTER IX [RESERVED]", "Chapter IX")]
    [TestCase("CHAPTER IV-U.S. IMMIGRATION", "Chapter IV")]
    public void ParseHeaderInToExpectedLabelLevel(string? input, string? expected)
    {
        var actual = XmlNodeProcessor.ParseHeaderInToExpectedLabelLevel(input);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
