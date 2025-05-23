// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable ConvertToConstant.Local

namespace Functional.Tests;

public sealed class ReadmeTests
{
    [Fact]
    public void Option_Match()
    {
        Option<int> some = Some(42);
        Option<int> none = None;

        int someValue = some.Match(Identity, 0);
        int noneValue = none.Match(Identity, 0);

        Assert.Equal(42, someValue);
        Assert.Equal(0, noneValue);
    }

    [Fact]
    public void List_Option_Choose()
    {
        List<Option<int>> input = [Some(1), None, Some(2)];

        List<int> output = input.Choose(Identity);

        Assert.Equal(2, output.Count);
    }

    [Fact]
    public void Result_Match()
    {
        Result<int, string> ok = Ok(42);
        Result<int, string> err = Err("There is no answer");

        int okValue = ok.Match(Identity, 0);
        int errValue = err.Match(Identity, 0);
        string error = err.Match(value => value.ToString(), Identity);

        Assert.Equal(42, okValue);
        Assert.Equal(0, errValue);
        Assert.Equal("There is no answer", error);
    }

    [Fact]
    public void Option_To_Result()
    {
        Option<int> some = Some(42);
        Option<int> none = None;

        Result<int, string> ok = some.ToResult("There is no answer");
        Result<int, string> err = none.ToResult(() => "There is no answer");

        Assert.True(ok.IsOk);
        Assert.True(err.IsErr);
        Assert.Equal("There is no answer", err.ExpectError());
    }

    [Fact]
    public void Result_To_Option()
    {
        Result<int, string> ok = Ok(42);
        Result<int, string> err = Err("There is no answer");

        Option<int> some = ok.ToOption();
        Option<int> none = err.ToOption();

        Assert.True(some.IsSome);
        Assert.True(none.IsNone);
    }

    [Fact]
    public void Nullable_Object_To_Option()
    {
        string? someString = "Forty two";
        string? nullString = null;

        Option<string> some = someString.ToOption();
        Option<string> none = nullString.ToOption();

        string someResult = some.Match(Identity, "none");
        string noneResult = none.Match(Identity, "none");

        Assert.Equal("Forty two", someResult);
        Assert.Equal("none", noneResult);
    }

    [Fact]
    public void Nullable_Value_To_Option()
    {
        int? someInt = 42;
        int? nullInt = null;

        Option<int> some = someInt.ToOption();
        Option<int> none = nullInt.ToOption();

        int someValue = some.Match(Identity, 0);
        int noneValue = none.Match(Identity, 0);

        Assert.Equal(42, someValue);
        Assert.Equal(0, noneValue);
    }

    [Fact]
    public void Option_To_Nullable_Object()
    {
        Option<string> some = Some("Forty two");
        Option<string> none = None;

        string? someValue = some.ToObject();
        string? noneValue = none.ToObject();

        Assert.Equal("Forty two", someValue);
        Assert.Null(noneValue);
    }

    [Fact]
    public void Option_To_Nullable_Value()
    {
        Option<int> some = Some(42);
        Option<int> none = None;

        int? someValue = some.ToNullable();
        int? noneValue = none.ToNullable();

        Assert.Equal(42, someValue);
        Assert.False(noneValue.HasValue);
    }

    [Fact]
    public void Result_To_Nullable_Object()
    {
        Result<string, string> ok = Ok("Forty two");
        Result<string, string> err = Err("There is no answer");

        string? okValue = ok.ToObject();
        string? errValue = err.ToObject();

        Assert.Equal("Forty two", okValue);
        Assert.Null(errValue);
    }

    [Fact]
    public void Result_To_Nullable_Value()
    {
        Result<int, string> ok = Ok(42);
        Result<int, string> err = Err("There is no answer");

        int? okValue = ok.ToNullable();
        int? errValue = err.ToNullable();

        Assert.Equal(42, okValue);
        Assert.False(errValue.HasValue);
    }
}