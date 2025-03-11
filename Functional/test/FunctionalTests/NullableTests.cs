namespace FunctionalTests;

public sealed class NullableTests
{
    [Fact]
    public void AsOption_Object_Returns_Some()
    {
        object obj = new { };

        var option = obj.ToOption();

        Assert.Same(obj, option.Unwrap());
    }

    [Fact]
    public void AsOption_Object_Null_Returns_None()
    {
        object obj = null!;

        var option = obj.ToOption();

        Assert.True(option.IsNone);
    }

    [Fact]
    public void AsOption_Nullable_Returns_Some()
    {
        int? nul = 42;

        var option = nul.ToOption();

        Assert.Equal(nul.Value, option.Unwrap());
    }

    [Fact]
    public void AsOption_Nullable_Null_Returns_None()
    {
        int? nul = null;

        var option = nul.ToOption();

        Assert.True(option.IsNone);
    }

    [Fact]
    public void ToObject_Some_Returns_Object()
    {
        object val = new { };

        Assert.Same(val, Option<object>.Some(val).ToObject());
    }

    [Fact]
    public void ToObject_None_Returns_Null()
    {
        Assert.Null(Option<object>.None.ToObject());
    }

    [Fact]
    public void ToNullable_Some_Returns_Value()
    {
        Assert.Equal(42, Option<int>.Some(42).ToNullable()!.Value);
    }

    [Fact]
    public void ToNullable_None_Returns_NoValue()
    {
        Assert.False(Option<int>.None.ToNullable().HasValue);
    }

    [Fact]
    public void ToObject_Ok_Returns_Object()
    {
        object val = new { };

        Assert.Same(val, Result<object, string>.Ok(val).ToObject());
    }

    [Fact]
    public void ToObject_Err_Returns_Null()
    {
        Assert.Null(Result<object, string>.Err("Error").ToObject());
    }

    [Fact]
    public void ToNullable_Ok_Returns_Value()
    {
        Assert.Equal(42, Result<int, string>.Ok(42).ToNullable()!.Value);
    }

    [Fact]
    public void ToNullable_Err_Returns_NoValue()
    {
        Assert.False(Result<int, string>.Err("Error").ToNullable().HasValue);
    }
}