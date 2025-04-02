namespace Functional.Tests;

public sealed class OptionExtensionsTests
{
    [Fact]
    public void Unwrap_Returns_Value()
    {
        Assert.Equal("Value", Option<string>.Some("Value").Unwrap());
    }

    [Fact]
    public void ExpectUnit_Returns_Unit()
    {
        Assert.Equal(Unit.Default, Option<string>.None.ExpectUnit());
    }

    [Fact]
    public void AsPure_Returns_Pure_Value()
    {
        Assert.Equal("Value", Option<string>.Some("Value").AsPure().Value);
    }

    [Fact]
    public void AsFail_Returns_Fail_Unit()
    {
        Assert.Equal(Unit.Default, Option<string>.None.AsFail().Error);
    }

    [Fact]
    public void ToResult_Some_Returns_Some_Value()
    {
        Assert.Equal("Value", Option<string>.Some("Value").ToResult("Error").Unwrap());
    }

    [Fact]
    public void ToResult_None_Returns_Error_Value()
    {
        Assert.Equal("Error", Option<string>.None.ToResult("Error").ExpectError());
    }

    [Fact]
    public void ToResult_Some_Does_Not_Call_Error_Function()
    {
        Assert.Equal("Value", Option<string>.Some("Value").ToResult(() => "Error").Unwrap());
    }

    [Fact]
    public void ToResult_None_Calls_Error_Function()
    {
        Assert.Equal("Error", Option<string>.None.ToResult(() => "Error").ExpectError());
    }

    [Fact]
    public void If_Some_Map_Calls_Func()
    {
        var func = new StubFunc<string, string>("Result");

        var result = Option<string>.Some("Some").Map(func.Invoke);

        Assert.True(func.Received);
        Assert.Equal("Some", func.Parameter);
        Assert.Equal(Option<string>.Some("Result"), result);
    }

    [Fact]
    public void If_None_Map_Does_Not_Call_Func()
    {
        var func = new StubFunc<string, string>("Result");

        var result = Option<string>.None.Map(func.Invoke);

        Assert.False(func.Received);
        Assert.Equal(Option<string>.None, result);
    }

    [Fact]
    public void If_Some_Bind_Calls_Func()
    {
        var func = new StubFunc<string, Option<string>>(Option<string>.Some("Result"));

        var result = Option<string>.Some("Some").Bind(func.Invoke);

        Assert.True(func.Received);
        Assert.Equal("Some", func.Parameter);
        Assert.Equal(Option<string>.Some("Result"), result);
    }

    [Fact]
    public void If_None_Bind_Does_Not_Call_Func()
    {
        var func = new StubFunc<string, Option<string>>(Option<string>.Some("Result"));

        var result = Option<string>.None.Bind(func.Invoke);

        Assert.False(func.Received);
        Assert.Equal(Option<string>.None, result);
    }

    [Fact]
    public void If_Some_Match_Calls_Some_Func()
    {
        var some = new StubFunc<string, string>("Result");
        var none = new StubFunc<string>("None");

        var result = Option<string>.Some("Some").Match(some.Invoke, none.Invoke);

        Assert.True(some.Received);
        Assert.False(none.Received);
        Assert.Equal("Some", some.Parameter);
        Assert.Equal("Result", result);
    }

    [Fact]
    public void If_None_Match_Calls_None_Func()
    {
        var some = new StubFunc<string, string>("Result");
        var none = new StubFunc<string>("None");

        var result = Option<string>.None.Match(some.Invoke, none.Invoke);

        Assert.False(some.Received);
        Assert.True(none.Received);
        Assert.Equal("None", result);
    }

    [Fact]
    public void If_Some_Match_Calls_Some_Func_2()
    {
        var some = new StubFunc<string, string>("Result");

        var result = Option<string>.Some("Some").Match(some.Invoke, "None");

        Assert.True(some.Received);
        Assert.Equal("Some", some.Parameter);
        Assert.Equal("Result", result);
    }

    [Fact]
    public void If_None_Match_Returns_None()
    {
        var some = new StubFunc<string, string>("Result");

        var result = Option<string>.None.Match(some.Invoke, "None");

        Assert.False(some.Received);
        Assert.Equal("None", result);
    }

    [Fact]
    public void If_Some_Match_Calls_Some_Action()
    {
        var some = new StubAction<string>();
        var none = new StubAction();

        Option<string>.Some("Some").Match(some.Invoke, none.Invoke);

        Assert.True(some.Received);
        Assert.False(none.Received);
        Assert.Equal("Some", some.Parameter);
    }

    [Fact]
    public void If_None_Match_Calls_None_Action()
    {
        var some = new StubAction<string>();
        var none = new StubAction();

        Option<string>.None.Match(some.Invoke, none.Invoke);

        Assert.False(some.Received);
        Assert.True(none.Received);
    }
}