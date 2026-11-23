using static Functional.Prelude;

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
    public void ToOption_Returns_Some_For_Nullable_Struct()
    {
        int? nullable = 42;

        Assert.Equal(Option<int>.Some(42), nullable.ToOption());
    }

    [Fact]
    public void ToOption_Returns_None_For_Nullable_Struct()
    {
        int? nullable = null;

        Assert.True(nullable.ToOption().IsNone);
    }

    [Fact]
    public void ToResult_Returns_Ok_For_Nullable_Struct()
    {
        int? nullable = 42;

        Assert.Equal("Ok(42)", nullable.ToResult().ToString());
    }

    [Fact]
    public void ToResult_Returns_Error_For_Nullable_Struct()
    {
        int? nullable = null;

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

    [Fact]
    public void Option_Transpose_Returns_Ok_For_Some()
    {
        var option = Option<Result<string, int>>.Some(Result<string, int>.Ok("text"));
        var result = option.Transpose();

        Assert.True(option.IsSome);
        Assert.True(result.IsOk);

        Assert.Equal("Some(Ok(text))", option.ToString());
        Assert.Equal("Ok(Some(text))", result.ToString());
    }

    [Fact]
    public void Option_Transpose_Returns_Error_For_Some_Error()
    {
        var option = Option<Result<string, int>>.Some(Result<string, int>.Error(42));
        var result = option.Transpose();

        Assert.True(option.IsSome);
        Assert.True(result.IsError);

        Assert.Equal("Some(Error(42))", option.ToString());
        Assert.Equal("Error(42)", result.ToString());
    }

    [Fact]
    public void Option_Transpose_Returns_None_For_None()
    {
        var option = Option<Result<string, int>>.None;
        var result = option.Transpose();

        Assert.True(option.IsNone);
        Assert.True(result.IsOk);

        Assert.Equal("None", option.ToString());
        Assert.Equal("Ok(None)", result.ToString());
    }

    [Fact]
    public void Result_Transpose_Returns_Some_For_Ok()
    {
        var result = Result<Option<string>, int>.Ok(Option<string>.Some("text"));
        var option = result.Transpose();

        Assert.True(result.IsOk);
        Assert.True(option.IsSome);

        Assert.Equal("Ok(Some(text))", result.ToString());
        Assert.Equal("Some(Ok(text))", option.ToString());
    }

    [Fact]
    public void Result_Transpose_Returns_None_For_Ok_None()
    {
        var result = Result<Option<string>, int>.Ok(Option<string>.None);
        var option = result.Transpose();

        Assert.True(result.IsOk);
        Assert.True(option.IsNone);

        Assert.Equal("Ok(None)", result.ToString());
        Assert.Equal("None", option.ToString());
    }

    [Fact]
    public void Result_Transpose_Returns_Some_Error_For_Error()
    {
        var result = Result<Option<string>, int>.Error(42);
        var option = result.Transpose();

        Assert.True(result.IsError);
        Assert.True(option.IsSome);

        Assert.Equal("Error(42)", result.ToString());
        Assert.Equal("Some(Error(42))", option.ToString());
    }

    [Fact]
    public void Chooser_Plus_Identity()
    {
        List<Option<int>> source = [Some(1), Nothing, Some(2)];

        var result = source.Choose(Identity);

        Assert.Equal([1, 2], result);
    }

    [Fact]
    public void Choose_Returns_Values_Of_Some()
    {
        List<string> source = ["1", "x", "3"];

        Assert.Equal([1, 3], source.Choose(ParseInt));
    }

    [Fact]
    public void Choose_Returns_Empty_When_All_None()
    {
        List<string> source = ["x", "y"];

        Assert.Empty(source.Choose(ParseInt));
    }

    private static Option<int> ParseInt(string text)
    {
        return int.TryParse(text, out var value) ? Option<int>.Some(value) : Option<int>.None;
    }

    [Fact]
    public void Prelude_Ignore()
    {
        Assert.Equal(default, 42.Ignore());
        Assert.Equal(default, "text".Ignore());
    }
}