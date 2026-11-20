using Functional;
using static Functional.Prelude;

// ReSharper disable once CheckNamespace
namespace Match.Tests;

public sealed class MatchTests
{
    private static Option<int> ParseInt(string text)
    {
        return int.TryParse(text, out var value) ? Some(value) : Nothing;
    }

    private static string Describe(Option<int> option)
    {
        return option switch
        {
            Some<int> { Value: > 100 } => "large",
            Some<int> => "small",
            None => "nothing"
        };
    }

    [Fact]
    public void Match_Some()
    {
        var result = ParseInt("42");
        var description = Describe(result);

        Assert.Equal("small", description);
    }

    [Fact]
    public void Match_None()
    {
        var result = ParseInt("not a number");
        var description = Describe(result);

        Assert.Equal("nothing", description);
    }
}