namespace FunctionalTests;

public sealed class ReadmeTests
{
    [Fact]
    public void Option()
    {
        Option<int> some = Some(42);
        Option<int> none = None;

        var value = some.Match(val => val, 0);
        var zero = none.Match(val => val, 0);

        Assert.Equal(42, value);
        Assert.Equal(0, zero);
    }

    [Fact]
    public void Result()
    {
        Result<int, string> ok = Ok(42);
        Result<int, string> error = Err("There is no answer");

        var value = ok.Match(val => val, 0);
        var zero = error.Match(val => val, 0);
        var message = error.Match(val => $"{val}", mes => mes);

        Assert.Equal(42, value);
        Assert.Equal(0, zero);
        Assert.Equal("There is no answer", message);
    }
}