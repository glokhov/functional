namespace Functional;

public static class OptionExtensions
{
    public static Pure<TValue> Pure<TValue>(this Option<TValue> option)
    {
        return (Pure<TValue>)option;
    }

    public static Fail<Unit> Fail<TValue>(this Option<TValue> option)
    {
        return (Fail<Unit>)option;
    }
}