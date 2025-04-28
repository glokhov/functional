namespace Functional.Tests;

public sealed class CollectionExtensionsTests
{
    [Fact]
    public void Test_Enumerable_Choose()
    {
        IEnumerable<Option<int>> input = [Some(1), None, Some(2)];

        var output = input.Choose(Prelude.Identity);

        Assert.Equal(2, output.Count());
    }

    [Fact]
    public void Test_List_Choose()
    {
        List<Option<int>> input = [Some(1), None, Some(2)];

        var output = input.Choose(Prelude.Identity);

        Assert.Equal(2, output.Count);
        Assert.Equal(1, output[0]);
        Assert.Equal(2, output[1]);
    }

    [Fact]
    public void Test_Array_Choose()
    {
        Option<int>[] input = [Some(1), None, Some(2)];

        var output = input.Choose(Prelude.Identity);

        Assert.Equal(2, output.Length);
        Assert.Equal(1, output[0]);
        Assert.Equal(2, output[1]);
    }


    [Fact]
    public void Test_Empty_Enumerable_Choose()
    {
        IEnumerable<Option<int>> input = [];

        var output = input.Choose(Prelude.Identity);

        Assert.Empty(output);
    }

    [Fact]
    public void Test_Empty_List_Choose()
    {
        List<Option<int>> input = [];

        var output = input.Choose(Prelude.Identity);

        Assert.Empty(output);
    }

    [Fact]
    public void Test_Empty_Array_Choose()
    {
        Option<int>[] input = [];

        var output = input.Choose(Prelude.Identity);

        Assert.Empty(output);
    }
}