namespace Functional;

public static class OptionExtensions
{
    public static Pure<TValue> AsPure<TValue>(this Option<TValue> option) where TValue : notnull
    {
        return (Pure<TValue>)option;
    }

    public static Fail<Unit> AsFail<TValue>(this Option<TValue> option) where TValue : notnull
    {
        return (Fail<Unit>)option;
    }
}