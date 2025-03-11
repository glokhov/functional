namespace Functional;

/// <summary>
/// Extension methods for nullable types.
/// </summary>
public static class Nullable
{
    /// <summary>
    /// Converts a potentially <c>null</c> value to an <c>Option</c> value.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns>The result <c>Option</c> value.</returns>
    public static Option<TValue> ToOption<TValue>(this TValue? value) where TValue : class
    {
        return value is not null ? Option<TValue>.Some(value) : Option<TValue>.None;
    }

    /// <summary>
    /// Converts a <c>Nullable</c> value to an <c>Option</c> value.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns>The result <c>Option</c> value.</returns>
    public static Option<TValue> ToOption<TValue>(this TValue? value) where TValue : struct
    {
        return value.HasValue ? Option<TValue>.Some(value.Value) : Option<TValue>.None;
    }

    /// <summary>
    /// Converts an <c>Option</c> value to a potentially <c>null</c> value.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns>The result value, which is null if the input was <c>None</c>.</returns>
    public static TValue? ToObject<TValue>(this Option<TValue> value) where TValue : class
    {
        return value.Match(some => some, (TValue?)null);
    }

    /// <summary>
    /// Convert an <c>Option</c> value to a <c>Nullable</c> value.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns>The result value.</returns>
    public static TValue? ToNullable<TValue>(this Option<TValue> value) where TValue : struct
    {
        return value.Match(some => some, new TValue?());
    }

    /// <summary>
    /// Converts a <c>Result</c> value to a potentially <c>null</c> value.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Ok</c> value.</typeparam>
    /// <typeparam name="TError">The type of the contained <c>Err</c> value.</typeparam>
    /// <returns>The result value, which is null if the input was <c>Err</c>.</returns>
    public static TValue? ToObject<TValue, TError>(this Result<TValue, TError> value)
        where TValue : class
        where TError : notnull
    {
        return value.ToOption().ToObject();
    }

    /// <summary>
    /// Convert a <c>Result</c> value to a <c>Nullable</c> value.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Ok</c> value.</typeparam>
    /// <typeparam name="TError">The type of the contained <c>Err</c> value.</typeparam>
    /// <returns>The result value.</returns>
    public static TValue? ToNullable<TValue, TError>(this Result<TValue, TError> value)
        where TValue : struct
        where TError : notnull
    {
        return value.ToOption().ToNullable();
    }
}