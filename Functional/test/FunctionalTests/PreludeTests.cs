namespace FunctionalTests;

public sealed class PreludeTests
{
    [Fact]
    public void Some_Returns_Pure_Value()
    {
        Assert.Equal(new Pure<string>("Value"), Some("Value"));
    }

    [Fact]
    public void None_Returns_Fail_Unit()
    {
        Assert.Equal(new Fail<Unit>(), None);
    }

    [Fact]
    public void Ok_Returns_Pure_Value()
    {
        Assert.Equal(new Pure<string>("Value"), Ok("Value"));
    }

    [Fact]
    public void Err_Returns_Fail_Error()
    {
        Assert.Equal(new Fail<string>("Error"), Err("Error"));
    }
}