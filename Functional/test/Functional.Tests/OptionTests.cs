namespace Functional.Tests;

public sealed class OptionTests
{
    [Fact]
    public void Default()
    {
        Option<string> option = default;

        Assert.False(option is None);
        Assert.False(option is Some<string>);

        Assert.Null(option.Value);
        Assert.Equal("", $"{option}");
    }

    [Fact]
    public void Some_Constructor_Creates_Some()
    {
        var option = new Option<string>("text");

        Assert.Equal("Some(text)", option.ToString());

        Assert.True(option is Some<string>);
        Assert.False(option is None);
    }

    [Fact]
    public void None_Constructor_Creates_None()
    {
        var option = new Option<string>(default(None));

        Assert.Equal("None", option.ToString());

        Assert.True(option is None);
        Assert.False(option is Some<string>);
    }

    [Fact]
    public void Option_Matches_On_Some()
    {
        Option<string> option = new Some<string>("text");

        Assert.True(option is Some<string>);
        Assert.False(option is None);

        if (option is Some<string> some)
        {
            Assert.Equal("text", some.Value);
        }
    }

    [Fact]
    public void Option_Matches_On_Some_Value()
    {
        Option<int> option = new Some<int>(42);

        Assert.True(option is Some<int>);
        Assert.False(option is None);

        if (option is Some<int> some)
        {
            Assert.Equal(42, some.Value);
        }
    }

    [Fact]
    public void Option_Matches_On_None()
    {
        Option<string> option = default(None);

        Assert.True(option is None);
        Assert.False(option is Some<string>);

        if (option is not None)
        {
            Assert.Fail();
        }
    }

    [Fact]
    public void Option_Binds_Returns_Some_For_Some()
    {
        Option<string> option = new Some<string>("text");

        var bound = option.Bind(value => (Option<int>)new Some<int>(value.Length));

        Assert.True(bound is Some<int>);
        Assert.False(bound is None);

        if (bound is Some<int> some)
        {
            Assert.Equal(4, some.Value);
        }
    }

    [Fact]
    public void Option_Binds_Returns_None_For_None()
    {
        Option<string> option = default(None);

        var bound = option.Bind(value => (Option<int>)new Some<int>(value.Length));

        Assert.True(bound is None);
        Assert.False(bound is Some<int>);

        Assert.True(bound is None);
    }

    [Fact]
    public void Option_Map_Returns_Some_For_Some()
    {
        Option<string> option = new Some<string>("text");

        var mapped = option.Map(value => value.Length);

        Assert.True(mapped is Some<int>);
        Assert.False(mapped is None);

        if (mapped is Some<int> some)
        {
            Assert.Equal(4, some.Value);
        }
    }

    [Fact]
    public void Option_Map_Returns_None_For_None()
    {
        Option<string> option = default(None);

        var mapped = option.Map(value => value.Length);

        Assert.True(mapped is None);
        Assert.False(mapped is Some<int>);

        Assert.True(mapped is None);
    }

    [Fact]
    public void Option_DefaultValue_Returns_Value_For_Some()
    {
        Option<string> option = new Some<string>("text");

        var value = option.Default("default");

        Assert.Equal("text", value);
    }

    [Fact]
    public void Option_DefaultValue_Returns_Default_For_None()
    {
        Option<string> option = default(None);

        var value = option.Default("default");

        Assert.Equal("default", value);
    }

    [Fact]
    public void Option_DefaultWith_Returns_Value_For_Some()
    {
        Option<string> option = new Some<string>("text");

        var value = option.Default(() => "default");

        Assert.Equal("text", value);
    }

    [Fact]
    public void Option_DefaultWith_Returns_Default_For_None()
    {
        Option<string> option = default(None);

        var value = option.Default(() => "default");

        Assert.Equal("default", value);
    }

    [Fact]
    public void Some_To_Option_Implicit()
    {
        Option<string> option = new Some<string>("text");

        Assert.True(option is Some<string>);
        Assert.False(option is None);

        if (option is Some<string> some)
        {
            Assert.Equal("text", some.Value);
        }
    }

    [Fact]
    public void Some_To_Option_Implicit_Value()
    {
        Option<int> option = new Some<int>(42);

        Assert.True(option is Some<int>);
        Assert.False(option is None);

        if (option is Some<int> some)
        {
            Assert.Equal(42, some.Value);
        }
    }

    [Fact]
    public void None_To_Option_Implicit()
    {
        Option<string> option = default(None);

        Assert.True(option is None);
        Assert.False(option is Some<string>);

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
        Option<string> option = null;

        if (option is not None)
        {
            Assert.Fail();
        }
    }

    [Fact]
    public void Option_To_Null_Implicit()
    {
        Option<string> option = null;
        string? value = option;

        Assert.Null(value);
    }

    [Fact]
    public void Option_Match_Action_Invokes_Some_Action_For_Some()
    {
        Option<int> option = new Some<int>(42);

        var someValue = 0;
        var noneCalled = false;

        option.Match(value => someValue = value, () => noneCalled = true);

        Assert.Equal(42, someValue);
        Assert.False(noneCalled);
    }

    [Fact]
    public void Option_Match_Action_Invokes_None_Action_For_None()
    {
        Option<int> option = default(None);

        var someCalled = false;
        var noneCalled = false;

        option.Match(_ => someCalled = true, () => noneCalled = true);

        Assert.False(someCalled);
        Assert.True(noneCalled);
    }

    [Fact]
    public void Option_Match_Func_Returns_Some_Result_For_Some()
    {
        Option<string> option = new Some<string>("text");

        var length = option.Match(value => value.Length, () => -1);

        Assert.Equal(4, length);
    }

    [Fact]
    public void Option_Match_Func_Returns_None_Result_For_None()
    {
        Option<string> option = default(None);

        var length = option.Match(value => value.Length, () => -1);

        Assert.Equal(-1, length);
    }

    [Fact]
    public void Option_Match_Func_Does_Not_Evaluate_None_Func_For_Some()
    {
        Option<string> option = new Some<string>("text");

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
        Option<string> option = default(None);

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
        Option<string> option = new Some<string>("text");

        var length = option.Match(value => value.Length, -1);

        Assert.Equal(4, length);
    }

    [Fact]
    public void Option_Match_Value_Returns_None_Value_For_None()
    {
        Option<string> option = default(None);

        var length = option.Match(value => value.Length, -1);

        Assert.Equal(-1, length);
    }
}