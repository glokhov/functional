namespace Functional.Tests;

public sealed class OkTests
{
    [Fact]
    public void Constructor_With_Null_Throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Ok<string>(null!));
    }

    [Fact]
    public void Constructor_With_Value_Sets_Value()
    {
        Assert.Equal("value", new Ok<string>("value").Value);
    }

    [Fact]
    public void ToString_Returns_Correct_Format()
    {
        Assert.Equal("Ok(value)", new Ok<string>("value").ToString());
    }

    [Fact]
    public void Implicit_Operator_Creates_Ok_From_Value()
    {
        Assert.Equal(new Ok<string>("value"), (Ok<string>)"value");
    }

    [Fact]
    public void Implicit_Operator_Creates_Value_From_Ok()
    {
        Assert.Equal("value", new Ok<string>("value"));
    }
}