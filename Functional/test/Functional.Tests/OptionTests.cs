namespace Functional.Tests;

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
        Assert.Equal("Value", pure.Value);
    }

    [Fact]
    public void If_None_Explicit_Operator_Pure_Throws_InvalidCastException()
    {
        Assert.Throws<InvalidCastException>(() => (Pure<string>)Option<string>.None);
    }

    [Fact]
    public void If_None_Explicit_Operator_Fail_Returns_Fail_Unit()
    {
        var fail = (Fail<Unit>)Option<string>.None;
        Assert.Equal(Unit.Default, fail.Error);
    }

    [Fact]
    public void If_Some_Explicit_Operator_Fail_Throws_InvalidCastException()
    {
        Assert.Throws<InvalidCastException>(() => (Fail<Unit>)Option<string>.Some("Value"));
    }
}