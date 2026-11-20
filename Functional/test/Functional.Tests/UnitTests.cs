namespace Functional.Tests;

public sealed class UnitTests
{
    [Fact]
    public void ToString_Returns_Correct_Format()
    {
        Assert.Equal("()", default(Unit).ToString());
    }
}