namespace FunctionalTests;

public sealed class UnitTests
{
    [Fact]
    public void ToString_Returns_Parenthesise()
    {
        Assert.Equal("()", Unit.Default.ToString());
    }
}