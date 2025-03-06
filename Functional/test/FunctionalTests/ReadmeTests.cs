// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace FunctionalTests;

public sealed class ReadmeTests
{
    [Fact]
    public void Option()
    {
        Option<int> some = Some(42);
        Option<int> none = None;

        int value = some.Match(val => val, 0);
        int zero = none.Match(val => val, 0);

        Assert.Equal(42, value);
        Assert.Equal(0, zero);
    }

    [Fact]
    public void Result()
    {
        Result<int, string> ok = Ok(42);
        Result<int, string> err = Err("There is no answer");

        int value = ok.Match(val => val, 0);
        int zero = err.Match(val => val, 0);
        string message = err.Match(val => $"{val}", mes => mes);

        Assert.Equal(42, value);
        Assert.Equal(0, zero);
        Assert.Equal("There is no answer", message);
    }
}