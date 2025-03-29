using NUnit.Framework;

namespace DogeServer.Tests.NUnit;

[SetUpFixture]
public class NunitSetup()
{
    [OneTimeSetUp]
    public void GlobalSetup()
    {
    }
}
