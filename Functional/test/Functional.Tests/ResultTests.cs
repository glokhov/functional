namespace Functional.Tests;

public sealed class ResultTests
{
    [Fact]
    public void Default()
    {
        Result<string, int> result = default;

        Assert.False(result.IsOk);
        Assert.False(result.IsError);
        Assert.Null(result.Value);
        Assert.Equal("", $"{result}");
    }

    [Fact]
    public void Default_Constructor_Nullable_Error_Throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Result<int, string>());
    }

    [Fact]
    public void Default_Constructor_Value_Error_Returns_Error()
    {
        var result = new Result<string, int>();

        Assert.True(result.IsError);
        Assert.False(result.IsOk);
        Assert.NotNull(result.Value);
        Assert.Equal("Error(0)", $"{result}");
    }

    [Fact]
    public void Ok_Constructor_Creates_Ok()
    {
        var result = new Result<string, int>("text");

        Assert.True(result.IsOk);
        Assert.False(result.IsError);
    }

    [Fact]
    public void Error_Constructor_Creates_Error()
    {
        var result = new Result<string, int>(42);

        Assert.True(result.IsError);
        Assert.False(result.IsOk);
    }

    [Fact]
    public void Result_Matches_On_Ok()
    {
        var result = Result<string, int>.Ok("text");

        Assert.True(result.IsOk);
        Assert.False(result.IsError);

        Assert.Equal("Ok(text)", $"{result}");

        if (result is Ok<string> ok)
        {
            Assert.Equal("text", ok.Value);
        }
    }

    [Fact]
    public void Result_Matches_On_Ok_Value()
    {
        var result = Result<int, string>.Ok(42);

        Assert.True(result.IsOk);
        Assert.False(result.IsError);

        Assert.Equal("Ok(42)", $"{result}");

        if (result is Ok<int> ok)
        {
            Assert.Equal(42, ok.Value);
        }
    }

    [Fact]
    public void Result_Matches_On_Error()
    {
        var result = Result<int, string>.Error("text");

        Assert.True(result.IsError);
        Assert.False(result.IsOk);

        Assert.Equal("Error(text)", result.ToString());

        if (result is Error<string> error)
        {
            Assert.Equal("text", error.Value);
        }
    }

    [Fact]
    public void Result_Matches_On_Error_Value()
    {
        var result = Result<string, int>.Error(42);

        Assert.True(result.IsError);
        Assert.False(result.IsOk);

        Assert.Equal("Error(42)", result.ToString());

        if (result is Error<int> error)
        {
            Assert.Equal(42, error.Value);
        }
    }

    [Fact]
    public void Result_Bind_Returns_Ok_For_Ok()
    {
        var result = Result<string, int>.Ok("text");
        var bound = result.Bind(value => Result<int, int>.Ok(value.Length));

        Assert.True(bound.IsOk);
        Assert.False(bound.IsError);

        if (bound is Ok<int> ok)
        {
            Assert.Equal(4, ok.Value);
        }
    }

    [Fact]
    public void Result_Bind_Returns_Error_For_Error()
    {
        var result = Result<int, string>.Error("error");
        var bound = result.Bind(value => Result<string, string>.Ok(value.ToString()));

        Assert.True(bound.IsError);
        Assert.False(bound.IsOk);

        if (bound is Error<string> error)
        {
            Assert.Equal("error", error.Value);
        }
    }

    [Fact]
    public void Result_Map_Returns_Ok_For_Ok()
    {
        var result = Result<string, int>.Ok("text");
        var mapped = result.Map(value => value.Length);

        Assert.True(mapped.IsOk);
        Assert.False(mapped.IsError);

        if (mapped is Ok<int> ok)
        {
            Assert.Equal(4, ok.Value);
        }
    }

    [Fact]
    public void Result_Map_Returns_Error_For_Error()
    {
        var result = Result<int, string>.Error("error");
        var mapped = result.Map(value => value.ToString());

        Assert.True(mapped.IsError);
        Assert.False(mapped.IsOk);

        if (mapped is Error<string> error)
        {
            Assert.Equal("error", error.Value);
        }
    }

    [Fact]
    public void Result_MapError_Returns_Ok_For_Ok()
    {
        var result = Result<string, int>.Ok("text");
        var mapped = result.MapError(value => value + 1);

        Assert.True(mapped.IsOk);
        Assert.False(mapped.IsError);

        if (mapped is Ok<string> ok)
        {
            Assert.Equal("text", ok.Value);
        }
    }

    [Fact]
    public void Result_MapError_Returns_Error_For_Error()
    {
        var result = Result<int, string>.Error("error");
        var mapped = result.MapError(value => value + "!");

        Assert.True(mapped.IsError);
        Assert.False(mapped.IsOk);

        if (mapped is Error<string> error)
        {
            Assert.Equal("error!", error.Value);
        }
    }

    [Fact]
    public void Result_DefaultValue_Returns_Ok_Value()
    {
        var result = Result<string, int>.Ok("text");

        Assert.True(result.IsOk);
        Assert.Equal("text", result.Default("default"));
    }

    [Fact]
    public void Result_DefaultValue_Returns_Default_Value_For_Error()
    {
        var result = Result<int, string>.Error("error");

        Assert.True(result.IsError);
        Assert.Equal(42, result.Default(42));
    }

    [Fact]
    public void Result_DefaultWith_Returns_Ok_Value()
    {
        var result = Result<string, int>.Ok("text");

        Assert.True(result.IsOk);
        Assert.Equal("text", result.Default(() => "default"));
    }

    [Fact]
    public void Result_DefaultWith_Returns_Default_Value_For_Error()
    {
        var result = Result<int, string>.Error("error");

        Assert.True(result.IsError);
        Assert.Equal(42, result.Default(() => 42));
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
    public void Ok_To_Result_Implicit_Conversion()
    {
        Result<string, int> result = new Ok<string>("text");

        Assert.True(result.IsOk);
        Assert.False(result.IsError);

        if (result is Ok<string> ok)
        {
            Assert.Equal("text", ok.Value);
        }
    }

    [Fact]
    public void Ok_To_Result_Implicit_Conversion_Value()
    {
        Result<int, string> result = new Ok<int>(42);

        Assert.True(result.IsOk);
        Assert.False(result.IsError);

        if (result is Ok<int> ok)
        {
            Assert.Equal(42, ok.Value);
        }
    }

    [Fact]
    public void Error_To_Result_Implicit_Conversion()
    {
        Result<int, string> result = new Error<string>("error");

        Assert.True(result.IsError);
        Assert.False(result.IsOk);

        if (result is Error<string> error)
        {
            Assert.Equal("error", error.Value);
        }
    }

    [Fact]
    public void Error_To_Result_Implicit_Conversion_Value()
    {
        Result<string, int> result = new Error<int>(42);

        Assert.True(result.IsError);
        Assert.False(result.IsOk);

        if (result is Error<int> error)
        {
            Assert.Equal(42, error.Value);
        }
    }

    [Fact]
    public void Result_Match_Action_Invokes_Ok_Action_For_Ok()
    {
        var result = Result<int, string>.Ok(42);
        var okValue = 0;
        string? errorValue = null;

        result.Match(value => okValue = value, error => errorValue = error);

        Assert.Equal(42, okValue);
        Assert.Null(errorValue);
    }

    [Fact]
    public void Result_Match_Action_Invokes_Error_Action_For_Error()
    {
        var result = Result<int, string>.Error("error");
        var okCalled = false;
        string? errorValue = null;

        result.Match(_ => okCalled = true, error => errorValue = error);

        Assert.False(okCalled);
        Assert.Equal("error", errorValue);
    }

    [Fact]
    public void Result_Match_Func_Returns_Ok_Result_For_Ok()
    {
        var result = Result<string, int>.Ok("text");
        var output = result.Match(value => value.Length, error => -error);

        Assert.Equal(4, output);
    }

    [Fact]
    public void Result_Match_Func_Returns_Error_Result_For_Error()
    {
        var result = Result<string, int>.Error(42);
        var output = result.Match(value => value.Length, error => -error);

        Assert.Equal(-42, output);
    }

    [Fact]
    public void Result_Match_Func_Does_Not_Evaluate_Error_Func_For_Ok()
    {
        var result = Result<string, int>.Ok("text");
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
        var result = Result<string, int>.Error(42);
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
        var result = Result<string, int>.Ok("text");
        var output = result.Match(value => value.Length, -1);

        Assert.Equal(4, output);
    }

    [Fact]
    public void Result_Match_Value_Returns_Error_Value_For_Error()
    {
        var result = Result<string, int>.Error(42);
        var output = result.Match(value => value.Length, -1);

        Assert.Equal(-1, output);
    }
}
