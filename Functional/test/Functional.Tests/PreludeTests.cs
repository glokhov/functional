namespace Functional.Tests;

public sealed class PreludeTests
{
    [Fact]
    public void Some_Returns_Pure_Value()
    {
        Assert.Equal(new Pure<string>("Value"), Some("Value"));
    }

    [Fact]
    public void None_Returns_Fail_Unit()
    {
        Assert.Equal(new Fail<Unit>(), None);
    }

    [Fact]
    public void Ok_Returns_Pure_Value()
    {
        Assert.Equal(new Pure<string>("Value"), Ok("Value"));
    }

    [Fact]
    public void Err_Returns_Fail_Error()
    {
        Assert.Equal(new Fail<string>("Error"), Err("Error"));
    }

    [Fact]
    public void If_Value_Identity_Returns_Same_Value()
    {
        const int some = 42;

        var result = Identity(some);

        Assert.Equal(some, result);
    }

    [Fact]
    public void If_Object_Identity_Returns_Same_Object()
    {
        const string some = "42";

        var result = Identity(some);

        Assert.Same(some, result);
    }
}