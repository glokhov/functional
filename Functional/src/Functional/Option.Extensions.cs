namespace Functional;

/// <summary>
/// Extensions for the type <c>Option</c>.
/// </summary>
public static class OptionExtensions
{
    /// <summary>
    /// Returns the contained <c>Some</c> value, consuming the <c>self</c> value.
    /// Because this function may throw an Exception, its use is generally discouraged.
    /// Instead, prefer to use the <c>Match</c> function.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Some</c> value.</typeparam>
    /// <returns>The contained <c>Some</c> value.</returns>
    /// <exception cref="System.InvalidCastException">self value is <c>None</c>.</exception>
    public static TValue Unwrap<TValue>(this Option<TValue> self) where TValue : notnull
    {
        return self.AsPure().Value;
    }

    /// <summary>
    /// Returns the <c>self</c> value as the <c>Pure</c> value.
    /// Because this function may throw an Exception, its use is generally discouraged.
    /// Instead, prefer to use the <c>Match</c> function.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Some</c> value.</typeparam>
    /// <returns>This <c>Option</c> value as a <c>Pure</c> value.</returns>
    /// <exception cref="System.InvalidCastException">self value is <c>None</c>.</exception>
    public static Pure<TValue> AsPure<TValue>(this Option<TValue> self) where TValue : notnull
    {
        return (Pure<TValue>)self;
    }
}