using System.Security.Cryptography.X509Certificates;

namespace MCIO.Core.UnitTests;

public class UnitTest1
{
    [Fact]
    public void HelloTest()
    {
        var sample = new Sample();
        var result = sample.Sum(10, 15);
        Assert.Equal(25, result);
    }
}