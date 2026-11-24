namespace Functional.Tests;

public sealed class ExtensionsTests
{
    [Fact]
    public void ToOption_Returns_Some()
    {
        Assert.True("text".ToOption() is Some<string>);
    }

    [Fact]
    public void ToOption_Returns_None()
    {
        string? nullable = null;

        Assert.True(nullable.ToOption() is None);
    }

    [Fact]
    public void ToResult_Returns_Ok()
    {
        Assert.True("text".ToResult() is Ok<string>);
    }

    [Fact]
    public void ToResult_Returns_Error()
    {
        string? nullable = null;

        Assert.True(nullable.ToResult() is Error<Unit>);
    }

    [Fact]
    public void ToOption_Returns_Some_For_Nullable_Struct()
    {
        int? nullable = 42;

        Assert.Equal(new Some<int>(42), nullable.ToOption());
    }

    [Fact]
    public void ToOption_Returns_None_For_Nullable_Struct()
    {
        int? nullable = null;

        Assert.True(nullable.ToOption() is None);
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

        Assert.True(nullable.ToResult() is Error<Unit>);
    }

    [Fact]
    public void Option_ToResult_Returns_Ok_For_Some()
    {
        Option<string> option = new Some<string>("text");
        var result = option.ToResult();

        Assert.True(option is Some<string>);
        Assert.True(result is Ok<string>);

        Assert.Equal("Some(text)", option.ToString());
        Assert.Equal("Ok(text)", result.ToString());
    }

    [Fact]
    public void Option_ToResult_Returns_Error_For_None()
    {
        Option<int> option = default(None);
        var result = option.ToResult();

        Assert.True(option is None);
        Assert.True(result is Error<Unit>);

        Assert.Equal("None", option.ToString());
        Assert.Equal("Error(())", result.ToString());
    }

    [Fact]
    public void Result_ToOption_Returns_Some_For_Ok()
    {
        Result<string, int> result = new Ok<string>("text");
        var option = result.ToOption();

        Assert.True(result is Ok<string>);
        Assert.True(option is Some<string>);
    }

    [Fact]
    public void Result_ToOption_Returns_None_For_Error()
    {
        Result<int, string> result = new Error<string>("error");
        var option = result.ToOption();

        Assert.True(result is Error<string>);
        Assert.True(option is None);
    }

    [Fact]
    public void Option_Transpose_Returns_Ok_For_Some()
    {
        Option<Result<string, int>> option = new Some<Result<string, int>>(new Ok<string>("text"));
        var result = option.Transpose();

        Assert.True(option is Some<Result<string, int>>);
        Assert.True(result is Ok<Option<string>>);

        Assert.Equal("Some(Ok(text))", option.ToString());
        Assert.Equal("Ok(Some(text))", result.ToString());
    }

    [Fact]
    public void Option_Transpose_Returns_Error_For_Some_Error()
    {
        Option<Result<string, int>> option = new Some<Result<string, int>>(new Error<int>(42));
        var result = option.Transpose();

        Assert.True(option is Some<Result<string, int>>);
        Assert.True(result is Error<int>);

        Assert.Equal("Some(Error(42))", option.ToString());
        Assert.Equal("Error(42)", result.ToString());
    }

    [Fact]
    public void Option_Transpose_Returns_None_For_None()
    {
        Option<Result<string, int>> option = default(None);
        var result = option.Transpose();

        Assert.True(option is None);
        Assert.True(result is Ok<Option<string>>);

        Assert.Equal("None", option.ToString());
        Assert.Equal("Ok(None)", result.ToString());
    }

    [Fact]
    public void Result_Transpose_Returns_Some_For_Ok()
    {
        Result<Option<string>, int> result = new Ok<Option<string>>(new Some<string>("text"));
        var option = result.Transpose();

        Assert.True(result is Ok<Option<string>>);
        Assert.True(option is Some<Result<string, int>>);

        Assert.Equal("Ok(Some(text))", result.ToString());
        Assert.Equal("Some(Ok(text))", option.ToString());
    }

    [Fact]
    public void Result_Transpose_Returns_None_For_Ok_None()
    {
        Result<Option<string>, int> result = new Ok<Option<string>>(default(None));
        var option = result.Transpose();

        Assert.True(result is Ok<Option<string>>);
        Assert.True(option is None);

        Assert.Equal("Ok(None)", result.ToString());
        Assert.Equal("None", option.ToString());
    }

    [Fact]
    public void Result_Transpose_Returns_Some_Error_For_Error()
    {
        Result<Option<string>, int> result = new Error<int>(42);
        var option = result.Transpose();

        Assert.True(result is Error<int>);
        Assert.True(option is Some<Result<string, int>>);

        Assert.Equal("Error(42)", result.ToString());
        Assert.Equal("Some(Error(42))", option.ToString());
    }

    [Fact]
    public void Choose_With_Identity_Returns_Values_Of_Some()
    {
        List<Option<int>> source = [new Some<int>(1), new None(), new Some<int>(2)];

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
        return int.TryParse(text, out var value) ? new Some<int>(value) : default(None);
    }

    private static Option<int> Identity(Option<int> option)
    {
        return option;
    }

    [Fact]
    public void Ignore_Returns_Unit()
    {
        Assert.Equal(default, 42.Ignore());
        Assert.Equal(default, "text".Ignore());
    }
}