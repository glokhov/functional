using Microsoft.FSharp.Core;

namespace Functional.FSharp.Tests;

public class ExtensionsTests
{
    [Fact]
    public void If_FSharpResult_Ok_ToResult_Returns_Ok()
    {
        Assert.Equal(42, FSharpResult<int, string>.NewOk(42).ToResult().Unwrap());
    }

    [Fact]
    public void If_FSharpResult_Error_ToResult_Returns_Error()
    {
        Assert.Equal("forty two", FSharpResult<int, string>.NewError("forty two").ToResult().ExpectError());
    }

    [Fact]
    public void If_FSharpOption_Some_ToOption_Returns_Some()
    {
        Assert.Equal(42, FSharpOption<int>.Some(42).ToOption().Unwrap());
    }

    [Fact]
    public void If_FSharpOption_None_ToOption_Returns_None()
    {
        Assert.True(FSharpOption<int>.None.ToOption().IsNone);
    }

    [Fact]
    public void If_FSharpValueOption_Some_ToOption_Returns_Some()
    {
        Assert.Equal(42, FSharpValueOption<int>.Some(42).ToOption().Unwrap());
    }

    [Fact]
    public void If_FSharpValueOption_None_ToOption_Returns_None()
    {
        Assert.True(FSharpValueOption<int>.None.ToOption().IsNone);
    }

    [Fact]
    public void If_Result_Ok_ToFSharpResult_Returns_Ok()
    {
        Assert.Equal(42, Result<int, string>.Ok(42).ToFSharpResult().ResultValue);
    }

    [Fact]
    public void If_Result_Error_ToFSharpResult_Returns_Error()
    {
        Assert.Equal("forty two", Result<int, string>.Err("forty two").ToFSharpResult().ErrorValue);
    }

    [Fact]
    public void If_Option_Some_ToFSharpOption_Returns_Some()
    {
        Assert.Equal(42, Option<int>.Some(42).ToFSharpOption().Value);
    }

    [Fact]
    public void If_Option_None_ToFSharpOption_Returns_None()
    {
        Assert.Null(Option<int>.None.ToFSharpOption());
    }

    [Fact]
    public void If_Option_Some_ToFSharpValueOption_Returns_Some()
    {
        Assert.Equal(42, Option<int>.Some(42).ToFSharpValueOption().Value);
    }

    [Fact]
    public void If_Option_None_ToFSharpValueOption_Returns_None()
    {
        Assert.True(Option<int>.None.ToFSharpValueOption().IsNone);
    }
}