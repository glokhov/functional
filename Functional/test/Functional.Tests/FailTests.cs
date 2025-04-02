namespace Functional.Tests;

public sealed class FailTests
{
    [Fact]
    public void ToString_Returns_Fail_Error()
    {
        Assert.Equal("Fail(Error)", new Fail<string>("Error").ToString());
    }
}