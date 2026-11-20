namespace Functional.Tests;

public sealed class NoneTests
{
    [Fact]
    public void ToString_Returns_Correct_Format()
    {
        Assert.Equal("None", default(None).ToString());
    }
}