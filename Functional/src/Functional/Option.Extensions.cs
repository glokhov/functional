namespace Functional;

public static class OptionExtensions
{
    public static TValue Unwrap<TValue>(this Option<TValue> self) where TValue : notnull
    {
        return self.AsPure().Value;
    }

    public static Pure<TValue> AsPure<TValue>(this Option<TValue> self) where TValue : notnull
    {
        return (Pure<TValue>)self;
    }
}