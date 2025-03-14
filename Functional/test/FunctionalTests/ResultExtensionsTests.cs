namespace FunctionalTests;

public sealed class ResultExtensionsTests
{
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
    public void ToOption_Ok_Returns_Some_Value()
    {
        Assert.Equal("Value", Result<string, string>.Ok("Value").ToOption().Unwrap());
    }

    [Fact]
    public void ToOption_Err_Returns_None()
    {
        Assert.Equal(Unit.Default, Result<string, string>.Err("Error").ToOption().ExpectUnit());
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
}