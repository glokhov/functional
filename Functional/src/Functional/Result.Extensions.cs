namespace Functional;

public static class ResultExtensions
{
    /// <summary>
    /// Returns the contained <c>Ok</c> value, consuming the <c>self</c> value.
    /// Because this function may throw an Exception, its use is generally discouraged.
    /// Instead, prefer to use the <c>Match</c> function.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Ok</c> value.</typeparam>
    /// <typeparam name="TError">The type of the contained <c>Err</c> value.</typeparam>
    /// <returns>The contained <c>Ok</c> value.</returns>
    /// <exception cref="System.InvalidCastException">self value equals <c>Err</c>.</exception>
    public static TValue Unwrap<TValue, TError>(this Result<TValue, TError> self) where TValue : notnull where TError : notnull
    {
        return self.AsPure().Value;
    }

    /// <summary>
    /// Returns the contained <c>Err</c> value, consuming the <c>self</c> value.
    /// Because this function may throw an Exception, its use is generally discouraged.
    /// Instead, prefer to use the <c>Match</c> function.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Ok</c> value.</typeparam>
    /// <typeparam name="TError">The type of the contained <c>Err</c> value.</typeparam>
    /// <returns>The contained <c>Err</c> value.</returns>
    /// <exception cref="System.InvalidCastException">self value equals <c>Ok</c>.</exception>
    public static TError ExpectError<TValue, TError>(this Result<TValue, TError> self) where TValue : notnull where TError : notnull
    {
        return self.AsFail().Error;
    }

    /// <summary>
    /// Returns the <c>self</c> value as the <c>Pure</c> value.
    /// Because this function may throw an Exception, its use is generally discouraged.
    /// Instead, prefer to use the <c>Match</c> function.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Some</c> value.</typeparam>
    /// <typeparam name="TError">The type of the contained <c>Err</c> value.</typeparam>
    /// <returns>This <c>Result</c> value as a <c>Pure</c> value.</returns>
    /// <exception cref="System.InvalidCastException">self value equals <c>Err</c>.</exception>
    public static Pure<TValue> AsPure<TValue, TError>(this Result<TValue, TError> self) where TValue : notnull where TError : notnull
    {
        return (Pure<TValue>)self;
    }

    /// <summary>
    /// Returns the <c>self</c> value as the <c>Err</c> value.
    /// Because this function may throw an Exception, its use is generally discouraged.
    /// Instead, prefer to use the <c>Match</c> function.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Some</c> value.</typeparam>
    /// <typeparam name="TError">The type of the contained <c>Err</c> value.</typeparam>
    /// <returns>This <c>Result</c> value as a <c>Err</c> value.</returns>
    /// <exception cref="System.InvalidCastException">self value equals <c>Ok</c>.</exception>
    public static Fail<TError> AsFail<TValue, TError>(this Result<TValue, TError> self) where TValue : notnull where TError : notnull
    {
        return (Fail<TError>)self;
    }
}