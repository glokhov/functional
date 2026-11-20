namespace Functional.Tests;

public sealed class SomeTests
{
    [Fact]
    public void Constructor_With_Null_Throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Some<string>(null!));
    }

    [Fact]
    public void Constructor_With_Value_Sets_Value()
    {
        Assert.Equal("value", new Some<string>("value").Value);
    }

    [Fact]
    public void ToString_Returns_Correct_Format()
    {
        Assert.Equal("Some(value)", new Some<string>("value").ToString());
    }

    [Fact]
    public void Implicit_Operator_Creates_Some_From_Value()
    {
        Assert.Equal(new Some<string>("value"), (Some<string>)"value");
    }

    [Fact]
    public void Implicit_Operator_Creates_Value_From_Some()
    {
        Assert.Equal("value", new Some<string>("value"));
    }
}