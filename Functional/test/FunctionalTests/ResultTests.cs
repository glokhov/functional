namespace FunctionalTests;

public sealed class ResultTests
{
    [Fact]
    public void If_Value_Is_Null_Ok_Throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Result<string, string>.Ok(null!));
    }

    [Fact]
    public void If_Error_Is_Null_Err_Throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Result<string, string>.Err(null!));
    }

    [Fact]
    public void If_Ok_IsOk_Returns_True()
    {
        Assert.True(Result<string, string>.Ok("Ok").IsOk);
    }

    [Fact]
    public void If_Err_IsOk_Returns_False()
    {
        Assert.False(Result<string, string>.Err("Err").IsOk);
    }

    [Fact]
    public void If_Ok_IsErr_Returns_False()
    {
        Assert.False(Result<string, string>.Ok("Ok").IsErr);
    }

    [Fact]
    public void If_Err_IsErr_Returns_True()
    {
        Assert.True(Result<string, string>.Err("Err").IsErr);
    }

    [Fact]
    public void If_Ok_Map_Calls_Func()
    {
        var func = new StubFunc<string, string>("Result");

        var result = Result<string, string>.Ok("Ok").Map(func.Invoke);

        Assert.True(func.Received);
        Assert.Equal("Ok", func.Parameter);
        Assert.Equal(Result<string, string>.Ok("Result"), result);
    }

    [Fact]
    public void If_Err_Map_Does_Not_Call_Func()
    {
        var func = new StubFunc<string, string>("Result");

        var result = Result<string, string>.Err("Err").Map(func.Invoke);

        Assert.False(func.Received);
        Assert.Equal(Result<string, string>.Err("Err"), result);
    }

    [Fact]
    public void If_Ok_Bind_Calls_Func()
    {
        var func = new StubFunc<string, Result<string, string>>(Result<string, string>.Ok("Result"));

        var result = Result<string, string>.Ok("Ok").Bind(func.Invoke);

        Assert.True(func.Received);
        Assert.Equal("Ok", func.Parameter);
        Assert.Equal(Result<string, string>.Ok("Result"), result);
    }

    [Fact]
    public void If_Err_Bind_Does_Not_Call_Func()
    {
        var func = new StubFunc<string, Result<string, string>>(Result<string, string>.Ok("Result"));

        var result = Result<string, string>.Err("Err").Bind(func.Invoke);

        Assert.False(func.Received);
        Assert.Equal(Result<string, string>.Err("Err"), result);
    }

    [Fact]
    public void If_Ok_Match_Calls_Ok_Func()
    {
        var ok = new StubFunc<string, string>("Result");
        var err = new StubFunc<string, string>("Error");

        var result = Result<string, string>.Ok("Ok").Match(ok.Invoke, err.Invoke);

        Assert.True(ok.Received);
        Assert.False(err.Received);
        Assert.Equal("Ok", ok.Parameter);
        Assert.Equal("Result", result);
    }

    [Fact]
    public void If_Err_Match_Calls_Error_Func()
    {
        var ok = new StubFunc<string, string>("Result");
        var err = new StubFunc<string, string>("Error");

        var result = Result<string, string>.Err("Err").Match(ok.Invoke, err.Invoke);

        Assert.False(ok.Received);
        Assert.True(err.Received);
        Assert.Equal("Err", err.Parameter);
        Assert.Equal("Error", result);
    }

    [Fact]
    public void If_Ok_Match_Calls_Ok_Func_2()
    {
        var ok = new StubFunc<string, string>("Result");

        var result = Result<string, string>.Ok("Ok").Match(ok.Invoke, "Error");

        Assert.True(ok.Received);
        Assert.Equal("Ok", ok.Parameter);
        Assert.Equal("Result", result);
    }

    [Fact]
    public void If_Err_Match_Returns_Error()
    {
        var ok = new StubFunc<string, string>("Result");

        var result = Result<string, string>.Err("Err").Match(ok.Invoke, "Error");

        Assert.False(ok.Received);
        Assert.Equal("Error", result);
    }

    [Fact]
    public void If_Ok_Match_Calls_Ok_Action()
    {
        var ok = new StubAction<string>();
        var err = new StubAction<string>();

        Result<string, string>.Ok("Ok").Match(ok.Invoke, err.Invoke);

        Assert.True(ok.Received);
        Assert.False(err.Received);
        Assert.Equal("Ok", ok.Parameter);
    }

    [Fact]
    public void If_Err_Match_Calls_Error_Action()
    {
        var ok = new StubAction<string>();
        var err = new StubAction<string>();

        Result<string, string>.Err("Err").Match(ok.Invoke, err.Invoke);

        Assert.False(ok.Received);
        Assert.True(err.Received);
        Assert.Equal("Err", err.Parameter);
    }

    [Fact]
    public void If_Ok_ToString_Returns_Ok_Value()
    {
        Assert.Equal("Ok(Value)", Result<string, string>.Ok("Value").ToString());
    }

    [Fact]
    public void If_Err_ToString_Returns_Err_Error()
    {
        Assert.Equal("Err(Error)", Result<string, string>.Err("Error").ToString());
    }

    [Fact]
    public void If_Pure_Implicit_Operator_Returns_Ok_Value()
    {
        Result<string, string> result = new Pure<string>("Value");
        Assert.Equal(Result<string, string>.Ok("Value"), result);
    }

    [Fact]
    public void If_Fail_Implicit_Operator_Returns_Err_Error()
    {
        Result<string, string> result = new Fail<string>("Error");
        Assert.Equal(Result<string, string>.Err("Error"), result);
    }

    [Fact]
    public void If_Some_Explicit_Operator_Pure_Returns_Pure_Value()
    {
        var pure = (Pure<string>)Result<string, string>.Ok("Value");
        Assert.Equal("Pure(Value)", pure.ToString());
    }

    [Fact]
    public void If_None_Explicit_Operator_Pure_Throws_InvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => (Pure<string>)Result<string, string>.Err("Error"));
    }

    [Fact]
    public void If_Error_Explicit_Operator_Fail_Returns_Fail_Error()
    {
        var fail = (Fail<string>)Result<string, string>.Err("Error");
        Assert.Equal("Fail(Error)", fail.ToString());
    }

    [Fact]
    public void If_Ok_Explicit_Operator_Fail_Throws_InvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => (Fail<string>)Result<string, string>.Ok("Value"));
    }

    [Fact]
    public void AsPure_Returns_Pure_Value()
    {
        Assert.Equal("Value", Result<string, string>.Ok("Value").AsPure().Value);
    }

    [Fact]
    public void AsFail_Returns_Fail_Error()
    {
        Assert.Equal("Error", Result<string, string>.Err("Error").AsFail().Error);
    }

    [Fact]
    public void Unwrap_Returns_Value()
    {
        Assert.Equal("Value", Result<string, string>.Ok("Value").Unwrap());
    }

    [Fact]
    public void ExpectError_Returns_Error()
    {
        Assert.Equal("Error", Result<string, string>.Err("Error").ExpectError());
    }
}