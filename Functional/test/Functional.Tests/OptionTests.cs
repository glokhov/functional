namespace Functional.Tests;

public sealed class OptionTests
{
    [Fact]
    public void Default()
    {
        Option<string> option = default;

        Assert.False(option.IsNone);
        Assert.False(option.IsSome);
        Assert.Null(option.Value);
        Assert.Equal("", $"{option}");
    }

    [Fact]
    public void Default_Constructor_Returns_None()
    {
        var option = new Option<string>();

        Assert.True(option.IsNone);
        Assert.False(option.IsSome);
        Assert.NotNull(option.Value);
        Assert.Equal("None", $"{option}");
    }

    [Fact]
    public void Some_Constructor_Creates_Some()
    {
        var option = new Option<string>("text");

        Assert.True(option.IsSome);
        Assert.False(option.IsNone);
    }

    [Fact]
    public void None_Constructor_Creates_None()
    {
       var option = new Option<string>(default(None));

        Assert.True(option.IsNone);
        Assert.False(option.IsSome);
    }

    [Fact]
    public void Option_Matches_On_Some()
    {
        var option = Option<string>.Some("text");

        Assert.True(option.IsSome);
        Assert.False(option.IsNone);

        Assert.Equal("Some(text)", $"{option}");

        if (option is Some<string> some)
        {
            Assert.Equal("text", some.Value);
        }
    }

    [Fact]
    public void Option_Matches_On_Some_Value()
    {
        var option = Option<int>.Some(42);

        Assert.True(option.IsSome);
        Assert.False(option.IsNone);

        Assert.Equal("Some(42)", option.ToString());

        if (option is Some<int> some)
        {
            Assert.Equal(42, some.Value);
        }
    }

    [Fact]
    public void Option_Matches_On_None()
    {
        var option = Option<string>.None;

        Assert.True(option.IsNone);
        Assert.False(option.IsSome);

        Assert.Equal("None", option.ToString());

        if (option is not None)
        {
            Assert.Fail();
        }
    }

    [Fact]
    public void Option_Binds_Returns_Some_For_Some()
    {
        var option = Option<string>.Some("text");
        var bound = option.Bind(value => Option<int>.Some(value.Length));

        Assert.True(bound.IsSome);
        Assert.False(bound.IsNone);

        if (bound is Some<int> some)
        {
            Assert.Equal(4, some.Value);
        }
    }

    [Fact]
    public void Option_Binds_Returns_None_For_None()
    {
        var option = Option<string>.None;
        var bound = option.Bind(value => Option<int>.Some(value.Length));

        Assert.True(bound.IsNone);
        Assert.False(bound.IsSome);

        Assert.True(bound is None);
    }

    [Fact]
    public void Option_Map_Returns_Some_For_Some()
    {
        var option = Option<string>.Some("text");
        var mapped = option.Map(value => value.Length);

        Assert.True(mapped.IsSome);
        Assert.False(mapped.IsNone);

        if (mapped is Some<int> some)
        {
            Assert.Equal(4, some.Value);
        }
    }

    [Fact]
    public void Option_Map_Returns_None_For_None()
    {
        var option = Option<string>.None;
        var mapped = option.Map(value => value.Length);

        Assert.True(mapped.IsNone);
        Assert.False(mapped.IsSome);

        Assert.True(mapped is None);
    }

    [Fact]
    public void Option_DefaultValue_Returns_Value_For_Some()
    {
        var option = Option<string>.Some("text");
        var value = option.Default("default");

        Assert.Equal("text", value);
    }

    [Fact]
    public void Option_DefaultValue_Returns_Default_For_None()
    {
        var option = Option<string>.None;
        var value = option.Default("default");

        Assert.Equal("default", value);
    }

    [Fact]
    public void Option_DefaultWith_Returns_Value_For_Some()
    {
        var option = Option<string>.Some("text");
        var value = option.Default(() => "default");

        Assert.Equal("text", value);
    }

    [Fact]
    public void Option_DefaultWith_Returns_Default_For_None()
    {
        var option = Option<string>.None;
        var value = option.Default(() => "default");

        Assert.Equal("default", value);
    }

    [Fact]
    public void Some_To_Option_Implicit()
    {
        Option<string> option = new Some<string>("text");

        Assert.True(option.IsSome);
        Assert.False(option.IsNone);

        if (option is Some<string> some)
        {
            Assert.Equal("text", some.Value);
        }
    }

    [Fact]
    public void Some_To_Option_Implicit_Value()
    {
        Option<int> option = new Some<int>(42);

        Assert.True(option.IsSome);
        Assert.False(option.IsNone);

        if (option is Some<int> some)
        {
            Assert.Equal(42, some.Value);
        }
    }

    [Fact]
    public void None_To_Option_Implicit()
    {
        Option<string> option = (None)default;

        Assert.True(option.IsNone);
        Assert.False(option.IsSome);

        if (option is not None)
        {
            Assert.Fail();
        }
    }

    [Fact]
    public void Object_To_Option_Implicit()
    {
        Option<string> option = "text";

        if (option is Some<string> some)
        {
            Assert.Equal("text", some.Value);
        }
    }

    [Fact]
    public void Option_To_Object_Implicit()
    {
        Option<string> option = "text";
        string? value = option;

        Assert.Equal("text", value);
    }

    [Fact]
    public void Value_To_Option_Implicit()
    {
        Option<int> option = 42;

        if (option is Some<int> some)
        {
            Assert.Equal(42, some.Value);
        }
    }

    [Fact]
    public void Option_To_Value_Implicit()
    {
        Option<int> option = 42;
        int value = option;

        Assert.Equal(42, value);
    }

    [Fact]
    public void Null_To_Option_Implicit()
    {
        Option<string> option = (string?)null;

        if (option is not None)
        {
            Assert.Fail();
        }
    }

    [Fact]
    public void Option_To_Null_Implicit()
    {
        Option<string> option = (string?)null;
        string? value = option;

        Assert.Null(value);
    }

    [Fact]
    public void Option_Match_Action_Invokes_Some_Action_For_Some()
    {
        var option = Option<int>.Some(42);
        var someValue = 0;
        var noneCalled = false;

        option.Match(value => someValue = value, () => noneCalled = true);

        Assert.Equal(42, someValue);
        Assert.False(noneCalled);
    }

    [Fact]
    public void Option_Match_Action_Invokes_None_Action_For_None()
    {
        var option = Option<int>.None;
        var someCalled = false;
        var noneCalled = false;

        option.Match(_ => someCalled = true, () => noneCalled = true);

        Assert.False(someCalled);
        Assert.True(noneCalled);
    }

    [Fact]
    public void Option_Match_Func_Returns_Some_Result_For_Some()
    {
        var option = Option<string>.Some("text");
        var length = option.Match(value => value.Length, () => -1);

        Assert.Equal(4, length);
    }

    [Fact]
    public void Option_Match_Func_Returns_None_Result_For_None()
    {
        var option = Option<string>.None;
        var length = option.Match(value => value.Length, () => -1);

        Assert.Equal(-1, length);
    }

    [Fact]
    public void Option_Match_Func_Does_Not_Evaluate_None_Func_For_Some()
    {
        var option = Option<string>.Some("text");
        var noneCalled = false;

        option.Match(value => value.Length, () =>
        {
            noneCalled = true;
            return -1;
        });

        Assert.False(noneCalled);
    }

    [Fact]
    public void Option_Match_Func_Does_Not_Evaluate_Some_Func_For_None()
    {
        var option = Option<string>.None;
        var someCalled = false;

        option.Match(value =>
        {
            someCalled = true;
            return value.Length;
        }, () => -1);

        Assert.False(someCalled);
    }

    [Fact]
    public void Option_Match_Value_Returns_Some_Result_For_Some()
    {
        var option = Option<string>.Some("text");
        var length = option.Match(value => value.Length, -1);

        Assert.Equal(4, length);
    }

    [Fact]
    public void Option_Match_Value_Returns_None_Value_For_None()
    {
        var option = Option<string>.None;
        var length = option.Match(value => value.Length, -1);

        Assert.Equal(-1, length);
    }
}
