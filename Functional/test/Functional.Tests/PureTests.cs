namespace Functional.Tests;

public sealed class PureTests
{
    [Fact]
    public void ToString_Returns_Pure_Value()
    {
        Assert.Equal("Pure(Value)", new Pure<string>("Value").ToString());
    }
}