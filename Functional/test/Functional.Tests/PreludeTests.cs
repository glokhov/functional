using static Functional.Prelude;

namespace Functional.Tests;

public sealed class PreludeTests
{
    [Fact]
    public void Prelude_Some()
    {
        Assert.Equal(Option<string>.Some("text"), Some("text"));
    }

    [Fact]
    public void Prelude_Some_Value()
    {
        Assert.Equal(Option<int>.Some(42), Some(42));
    }

    [Fact]
    public void Prelude_None()
    {
        Assert.Equal(Option<string>.None, Nothing);
    }

    [Fact]
    public void Prelude_Ok()
    {
        Assert.Equal(Result<string, int>.Ok("text"), Ok("text"));
    }

    [Fact]
    public void Prelude_Ok_Value()
    {
        Assert.Equal(Result<int, string>.Ok(42), Ok(42));
    }

    [Fact]
    public void Prelude_Error()
    {
        Assert.Equal(Result<int, string>.Error("error"), Error("error"));
    }

    [Fact]
    public void Prelude_Error_Value()
    {
        Assert.Equal(Result<string, int>.Error(42), Error(42));
    }
}