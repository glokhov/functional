namespace Functional;

/// <summary>
/// Extension methods for nullable types.
/// </summary>
public static class NullableExtensions
{
    /// <summary>
    /// Converts a potentially <c>null</c> value to an <c>Option</c>.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns>The value as an <c>Option</c>.</returns>
    public static Option<TValue> ToOption<TValue>(this TValue? value) where TValue : class
    {
        return value is not null ? Option<TValue>.Some(value) : Option<TValue>.None;
    }

    /// <summary>
    /// Converts a <c>Nullable</c> value to an <c>Option</c>.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns>The value as an <c>Option</c>.</returns>
    public static Option<TValue> ToOption<TValue>(this TValue? value) where TValue : struct
    {
        return value.HasValue ? Option<TValue>.Some(value.Value) : Option<TValue>.None;
    }

    /// <summary>
    /// Converts a potentially <c>null</c> value to a <c>Result</c>.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <param name="error">The error value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <returns>The value as a <c>Result</c>.</returns>
    public static Result<TValue, TError> ToResult<TValue, TError>(this TValue? value, TError error)
        where TValue : class
        where TError : notnull
    {
        return value is not null ? Result<TValue, TError>.Ok(value) : Result<TValue, TError>.Err(error);
    }

    /// <summary>
    /// Converts a <c>Nullable</c> value to a <c>Result</c>.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <param name="error">The error value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <returns>The value as a <c>Result</c>.</returns>
    public static Result<TValue, TError> ToResult<TValue, TError>(this TValue? value, TError error)
        where TValue : struct
        where TError : notnull
    {
        return value.HasValue ? Result<TValue, TError>.Ok(value.Value) : Result<TValue, TError>.Err(error);
    }

    /// <summary>
    /// Converts a potentially <c>null</c> value to a <c>Result</c>.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <param name="error">The error value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <returns>The value as a <c>Result</c>.</returns>
    public static Result<TValue, TError> ToResult<TValue, TError>(this TValue? value, Func<TError> error)
        where TValue : class
        where TError : notnull
    {
        return value is not null ? Result<TValue, TError>.Ok(value) : Result<TValue, TError>.Err(error());
    }

    /// <summary>
    /// Converts a <c>Nullable</c> value to a <c>Result</c>.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <param name="error">The error value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <returns>The value as a <c>Result</c>.</returns>
    public static Result<TValue, TError> ToResult<TValue, TError>(this TValue? value, Func<TError> error)
        where TValue : struct
        where TError : notnull
    {
        return value.HasValue ? Result<TValue, TError>.Ok(value.Value) : Result<TValue, TError>.Err(error());
    }

    /// <summary>
    /// Converts an <c>Option</c> to a potentially <c>null</c> value.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns>The contained value, which is <c>null</c> if the input was <c>None</c>.</returns>
    public static TValue? ToObject<TValue>(this Option<TValue> value) where TValue : class
    {
        return value.Match(some => some, (TValue?)null);
    }

    /// <summary>
    /// Convert an <c>Option</c> to a <c>Nullable</c> value.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns>The contained value, which is <c>TValue?</c> if the input was <c>None</c>.</returns>
    public static TValue? ToNullable<TValue>(this Option<TValue> value) where TValue : struct
    {
        return value.Match(some => some, new TValue?());
    }

    /// <summary>
    /// Converts a <c>Result</c> to a potentially <c>null</c> value.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <returns>The success value, which is <c>null</c> if the input was an <c>Err</c>.</returns>
    public static TValue? ToObject<TValue, TError>(this Result<TValue, TError> value)
        where TValue : class
        where TError : notnull
    {
        return value.ToOption().ToObject();
    }

    /// <summary>
    /// Convert a <c>Result</c> to a <c>Nullable</c> value.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <returns>The success value, which is <c>TValue?</c> if the input was <c>None</c>.</returns>
    public static TValue? ToNullable<TValue, TError>(this Result<TValue, TError> value)
        where TValue : struct
        where TError : notnull
    {
        return value.ToOption().ToNullable();
    }
}