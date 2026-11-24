namespace Functional.Tests;

public sealed class ResultTests
{
    [Fact]
    public void Default()
    {
        Result<string, int> result = default;

        Assert.False(result is Ok<string>);
        Assert.False(result is Error<int>);

        Assert.Null(result.Value);
        Assert.Equal("", $"{result}");
    }

    [Fact]
    public void Ok_Constructor_Creates_Ok()
    {
        var result = new Result<string, int>("text");

        Assert.Equal("Ok(text)", result.ToString());

        Assert.True(result is Ok<string>);
        Assert.False(result is Error<int>);
    }

    [Fact]
    public void Error_Constructor_Creates_Error()
    {
        var result = new Result<string, int>(42);

        Assert.Equal("Error(42)", result.ToString());

        Assert.True(result is Error<int>);
        Assert.False(result is Ok<string>);
    }

    [Fact]
    public void Result_Matches_On_Ok()
    {
        Result<string, int> result = new Ok<string>("text");

        Assert.True(result is Ok<string>);
        Assert.False(result is Error<int>);

        if (result is Ok<string> ok)
        {
            Assert.Equal("text", ok.Value);
        }
    }

    [Fact]
    public void Result_Matches_On_Ok_Value()
    {
        Result<int, string> result = new Ok<int>(42);

        Assert.True(result is Ok<int>);
        Assert.False(result is Error<string>);

        if (result is Ok<int> ok)
        {
            Assert.Equal(42, ok.Value);
        }
    }

    [Fact]
    public void Result_Matches_On_Error()
    {
        Result<int, string> result = new Error<string>("text");

        Assert.True(result is Error<string>);
        Assert.False(result is Ok<int>);

        if (result is Error<string> error)
        {
            Assert.Equal("text", error.Value);
        }
    }

    [Fact]
    public void Result_Matches_On_Error_Value()
    {
        Result<string, int> result = new Error<int>(42);

        Assert.True(result is Error<int>);
        Assert.False(result is Ok<string>);

        if (result is Error<int> error)
        {
            Assert.Equal(42, error.Value);
        }
    }

    [Fact]
    public void Result_Bind_Returns_Ok_For_Ok()
    {
        Result<string, int> result = new Ok<string>("text");

        var bound = result.Bind(value => (Result<int, int>)new Ok<int>(value.Length));

        Assert.True(bound is Ok<int>);
        Assert.False(bound is Error<int>);

        if (bound is Ok<int> ok)
        {
            Assert.Equal(4, ok.Value);
        }
    }

    [Fact]
    public void Result_Bind_Returns_Error_For_Error()
    {
        Result<int, string> result = new Error<string>("error");

        var bound = result.Bind(value => (Result<string, string>)new Ok<string>(value.ToString()));

        Assert.True(bound is Error<string>);
        Assert.False(bound is Ok<string>);

        if (bound is Error<string> error)
        {
            Assert.Equal("error", error.Value);
        }
    }

    [Fact]
    public void Result_Map_Returns_Ok_For_Ok()
    {
        Result<string, int> result = new Ok<string>("text");

        var mapped = result.Map(value => value.Length);

        Assert.True(mapped is Ok<int>);
        Assert.False(mapped is Error<int>);

        if (mapped is Ok<int> ok)
        {
            Assert.Equal(4, ok.Value);
        }
    }

    [Fact]
    public void Result_Map_Returns_Error_For_Error()
    {
        Result<int, string> result = new Error<string>("error");

        var mapped = result.Map(value => value.ToString());

        Assert.True(mapped is Error<string>);
        Assert.False(mapped is Ok<string>);

        if (mapped is Error<string> error)
        {
            Assert.Equal("error", error.Value);
        }
    }

    [Fact]
    public void Result_MapError_Returns_Ok_For_Ok()
    {
        Result<string, int> result = new Ok<string>("text");

        var mapped = result.MapError(value => value + 1);

        Assert.True(mapped is Ok<string>);
        Assert.False(mapped is Error<int>);

        if (mapped is Ok<string> ok)
        {
            Assert.Equal("text", ok.Value);
        }
    }

    [Fact]
    public void Result_MapError_Returns_Error_For_Error()
    {
        Result<int, string> result = new Error<string>("error");

        var mapped = result.MapError(value => value + "!");

        Assert.True(mapped is Error<string>);
        Assert.False(mapped is Ok<int>);

        if (mapped is Error<string> error)
        {
            Assert.Equal("error!", error.Value);
        }
    }

    [Fact]
    public void Result_DefaultValue_Returns_Ok_Value()
    {
        Result<string, int> result = new Ok<string>("text");

        Assert.True(result is Ok<string>);
        Assert.Equal("text", result.Default("default"));
    }

    [Fact]
    public void Result_DefaultValue_Returns_Default_Value_For_Error()
    {
        Result<int, string> result = new Error<string>("error");

        Assert.True(result is Error<string>);
        Assert.Equal(42, result.Default(42));
    }

    [Fact]
    public void Result_DefaultWith_Returns_Ok_Value()
    {
        Result<string, int> result = new Ok<string>("text");

        Assert.True(result is Ok<string>);
        Assert.Equal("text", result.Default(_ => "default"));
    }

    [Fact]
    public void Result_DefaultWith_Returns_Default_Value_For_Error()
    {
        Result<int, string> result = new Error<string>("error");

        Assert.True(result is Error<string>);
        Assert.Equal(42, result.Default(_ => 42));
    }

    [Fact]
    public void Ok_To_Result_Implicit_Conversion()
    {
        Result<string, int> result = new Ok<string>("text");

        Assert.True(result is Ok<string>);
        Assert.False(result is Error<int>);

        if (result is Ok<string> ok)
        {
            Assert.Equal("text", ok.Value);
        }
    }

    [Fact]
    public void Ok_To_Result_Implicit_Conversion_Value()
    {
        Result<int, string> result = new Ok<int>(42);

        Assert.True(result is Ok<int>);
        Assert.False(result is Error<string>);

        if (result is Ok<int> ok)
        {
            Assert.Equal(42, ok.Value);
        }
    }

    [Fact]
    public void Error_To_Result_Implicit_Conversion()
    {
        Result<int, string> result = new Error<string>("error");

        Assert.True(result is Error<string>);
        Assert.False(result is Ok<int>);

        if (result is Error<string> error)
        {
            Assert.Equal("error", error.Value);
        }
    }

    [Fact]
    public void Error_To_Result_Implicit_Conversion_Value()
    {
        Result<string, int> result = new Error<int>(42);

        Assert.True(result is Error<int>);
        Assert.False(result is Ok<string>);

        if (result is Error<int> error)
        {
            Assert.Equal(42, error.Value);
        }
    }

    [Fact]
    public void Result_Match_Action_Invokes_Ok_Action_For_Ok()
    {
        Result<int, string> result = new Ok<int>(42);

        var okValue = 0;
        string? errorValue = null;

        result.Match(value => okValue = value, error => errorValue = error);

        Assert.Equal(42, okValue);
        Assert.Null(errorValue);
    }

    [Fact]
    public void Result_Match_Action_Invokes_Error_Action_For_Error()
    {
        Result<int, string> result = new Error<string>("error");

        var okCalled = false;
        string? errorValue = null;

        result.Match(_ => okCalled = true, error => errorValue = error);

        Assert.False(okCalled);
        Assert.Equal("error", errorValue);
    }

    [Fact]
    public void Result_Match_Func_Returns_Ok_Result_For_Ok()
    {
        Result<string, int> result = new Ok<string>("text");

        var output = result.Match(value => value.Length, error => -error);

        Assert.Equal(4, output);
    }

    [Fact]
    public void Result_Match_Func_Returns_Error_Result_For_Error()
    {
        Result<string, int> result = new Error<int>(42);

        var output = result.Match(value => value.Length, error => -error);

        Assert.Equal(-42, output);
    }

    [Fact]
    public void Result_Match_Func_Does_Not_Evaluate_Error_Func_For_Ok()
    {
        Result<string, int> result = new Ok<string>("text");

        var errorCalled = false;

        result.Match(value => value.Length, error =>
        {
            errorCalled = true;
            return error;
        });

        Assert.False(errorCalled);
    }

    [Fact]
    public void Result_Match_Func_Does_Not_Evaluate_Ok_Func_For_Error()
    {
        Result<string, int> result = new Error<int>(42);

        var okCalled = false;

        result.Match(value =>
        {
            okCalled = true;
            return value.Length;
        }, error => error);

        Assert.False(okCalled);
    }

    [Fact]
    public void Result_Match_Value_Returns_Ok_Result_For_Ok()
    {
        Result<string, int> result = new Ok<string>("text");

        var output = result.Match(value => value.Length, -1);

        Assert.Equal(4, output);
    }

    [Fact]
    public void Result_Match_Value_Returns_Error_Value_For_Error()
    {
        Result<string, int> result = new Error<int>(42);

        var output = result.Match(value => value.Length, -1);

        Assert.Equal(-1, output);
    }
}