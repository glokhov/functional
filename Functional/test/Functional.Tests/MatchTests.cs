using System.Globalization;
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

    private static Result<double, string> ParseDouble(string text)
    {
        return double.TryParse(text, CultureInfo.InvariantCulture, out var value) ? Ok(value) : Error("not a number");
    }

    private static string Describe(Result<double, string> result)
    {
        return result switch
        {
            Ok<double> { Value: > 100 } => "large",
            Ok<double> { Value: var value } => $"{value}",
            Error<string> error => error.Value
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

    [Fact]
    public void Match_Ok()
    {
        var result = ParseDouble("42.0");
        var description = Describe(result);

        Assert.Equal("42", description);
    }

    [Fact]
    public void Match_Ok_Property()
    {
        var result = ParseDouble("101.0");
        var description = Describe(result);

        Assert.Equal("large", description);
    }

    [Fact]
    public void Match_Error()
    {
        var result = ParseDouble("not a number");
        var description = Describe(result);

        Assert.Equal("not a number", description);
    }
}