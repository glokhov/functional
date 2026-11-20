namespace Functional.Tests;

public sealed class ErrorTests
{
    [Fact]
    public void Constructor_With_Null_Throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Error<string>(null!));
    }

    [Fact]
    public void Constructor_With_Value_Sets_Value()
    {
        Assert.Equal("error", new Error<string>("error").Value);
    }

    [Fact]
    public void ToString_Returns_Correct_Format()
    {
        Assert.Equal("Error(error)", new Error<string>("error").ToString());
    }

    [Fact]
    public void Implicit_Operator_Creates_Error_From_Value()
    {
        Assert.Equal(new Error<string>("error"), (Error<string>)"error");
    }

    [Fact]
    public void Implicit_Operator_Creates_Value_From_Error()
    {
        Assert.Equal("error", new Error<string>("error"));
    }
}