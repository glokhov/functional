namespace Functional.Tests;

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
    public void Default_Is_Error()
    {
        Result<string, string> result = default;
        Assert.True(result.IsErr);
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
    public void If_Ok_Explicit_Operator_Pure_Returns_Pure_Value()
    {
        var pure = (Pure<string>)Result<string, string>.Ok("Value");
        Assert.Equal("Value", pure.Value);
    }

    [Fact]
    public void If_Error_Explicit_Operator_Pure_Throws_InvalidCastException()
    {
        Assert.Throws<InvalidCastException>(() => (Pure<string>)Result<string, string>.Err("Error"));
    }

    [Fact]
    public void If_Error_Explicit_Operator_Fail_Returns_Fail_Error()
    {
        var fail = (Fail<string>)Result<string, string>.Err("Error");
        Assert.Equal("Error", fail.Error);
    }

    [Fact]
    public void If_Ok_Explicit_Operator_Fail_Throws_InvalidCastException()
    {
        Assert.Throws<InvalidCastException>(() => (Fail<string>)Result<string, string>.Ok("Value"));
    }
}