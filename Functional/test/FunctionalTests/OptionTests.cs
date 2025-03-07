namespace FunctionalTests;

public sealed class OptionTests
{
    [Fact]
    public void If_Value_Is_Null_Some_Throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Option<string>.Some(null!));
    }

    [Fact]
    public void If_Some_IsSome_Returns_True()
    {
        Assert.True(Option<string>.Some("Some").IsSome);
    }

    [Fact]
    public void If_None_IsSome_Returns_False()
    {
        Assert.False(Option<string>.None.IsSome);
    }

    [Fact]
    public void If_Some_IsNone_Returns_False()
    {
        Assert.False(Option<string>.Some("Some").IsNone);
    }

    [Fact]
    public void If_None_IsNone_Returns_True()
    {
        Assert.True(Option<string>.None.IsNone);
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

    [Fact]
    public void If_Some_ToString_Returns_Some_Value()
    {
        Assert.Equal("Some(Value)", Option<string>.Some("Value").ToString());
    }

    [Fact]
    public void If_None_ToString_Returns_None()
    {
        Assert.Equal("None", Option<string>.None.ToString());
    }

    [Fact]
    public void If_Pure_Implicit_Operator_Returns_Some_Value()
    {
        Option<string> option = new Pure<string>("Value");
        Assert.Equal(Option<string>.Some("Value"), option);
    }

    [Fact]
    public void If_Fail_Implicit_Operator_Returns_None()
    {
        Option<string> option = new Fail<Unit>(Unit.Default);
        Assert.Equal(Option<string>.None, option);
    }

    [Fact]
    public void If_Some_Explicit_Operator_Pure_Returns_Pure_Value()
    {
        var pure = (Pure<string>)Option<string>.Some("Value");
        Assert.Equal("Pure(Value)", pure.ToString());
    }

    [Fact]
    public void If_None_Explicit_Operator_Pure_Throws_InvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => (Pure<string>)Option<string>.None);
    }

    [Fact]
    public void AsPure_Returns_Pure_Value()
    {
        Assert.Equal("Value", Option<string>.Some("Value").AsPure().Value);
    }

    [Fact]
    public void Unwrap_Returns_Value()
    {
        Assert.Equal("Value", Option<string>.Some("Value").Unwrap());
    }
}