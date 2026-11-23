namespace Functional.Tests;

public sealed class ExtensionsTests
{
    [Fact]
    public void ToOption_Returns_Some()
    {
        Assert.True("text".ToOption().IsSome);
    }

    [Fact]
    public void ToOption_Returns_None()
    {
        string? nullable = null;

        Assert.True(nullable.ToOption().IsNone);
    }

    [Fact]
    public void ToResult_Returns_Ok()
    {
        Assert.True("text".ToResult().IsOk);
    }

    [Fact]
    public void ToResult_Returns_Error()
    {
        string? nullable = null;

        Assert.True(nullable.ToResult().IsError);
    }

    [Fact]
    public void Option_ToResult_Returns_Ok_For_Some()
    {
        var option = Option<string>.Some("text");
        var result = option.ToResult();

        Assert.True(option.IsSome);
        Assert.True(result.IsOk);

        Assert.Equal("Some(text)", option.ToString());
        Assert.Equal("Ok(text)", result.ToString());
    }

    [Fact]
    public void Option_ToResult_Returns_Error_For_None()
    {
        var option = Option<int>.None;
        var result = option.ToResult();

        Assert.True(option.IsNone);
        Assert.True(result.IsError);

        Assert.Equal("None", option.ToString());
        Assert.Equal("Error(())", result.ToString());
    }

    [Fact]
    public void Result_ToOption_Returns_Some_For_Ok()
    {
        var result = Result<string, int>.Ok("text");
        var option = result.ToOption();

        Assert.True(result.IsOk);
        Assert.True(option.IsSome);
    }

    [Fact]
    public void Result_ToOption_Returns_None_For_Error()
    {
        var result = Result<int, string>.Error("error");
        var option = result.ToOption();

        Assert.True(result.IsError);
        Assert.True(option.IsNone);
    }
}