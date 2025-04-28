namespace Functional;

/// <summary>
/// Extensions for the type <c>Result</c>.
/// </summary>
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
    /// <exception cref="System.InvalidCastException">self value is <c>Err</c>.</exception>
    public static TValue Unwrap<TValue, TError>(this Result<TValue, TError> self)
        where TValue : notnull
        where TError : notnull
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
    /// <exception cref="System.InvalidCastException">self value is <c>Ok</c>.</exception>
    public static TError ExpectError<TValue, TError>(this Result<TValue, TError> self)
        where TValue : notnull
        where TError : notnull
    {
        return self.AsFail().Error;
    }

    /// <summary>
    /// Returns the <c>self</c> value as the <c>Pure</c> value.
    /// Because this function may throw an Exception, its use is generally discouraged.
    /// Instead, prefer to use the <c>Match</c> function.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Ok</c> value.</typeparam>
    /// <typeparam name="TError">The type of the contained <c>Err</c> value.</typeparam>
    /// <returns>This <c>Result</c> value as a <c>Pure</c> value.</returns>
    /// <exception cref="System.InvalidCastException">self value is <c>Err</c>.</exception>
    public static Pure<TValue> AsPure<TValue, TError>(this Result<TValue, TError> self)
        where TValue : notnull
        where TError : notnull
    {
        return (Pure<TValue>)self;
    }

    /// <summary>
    /// Returns the <c>self</c> value as the <c>Err</c> value.
    /// Because this function may throw an Exception, its use is generally discouraged.
    /// Instead, prefer to use the <c>Match</c> function.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Ok</c> value.</typeparam>
    /// <typeparam name="TError">The type of the contained <c>Err</c> value.</typeparam>
    /// <returns>This <c>Result</c> value as a <c>Err</c> value.</returns>
    /// <exception cref="System.InvalidCastException">self value is <c>Ok</c>.</exception>
    public static Fail<TError> AsFail<TValue, TError>(this Result<TValue, TError> self)
        where TValue : notnull
        where TError : notnull
    {
        return (Fail<TError>)self;
    }

    /// <summary>
    /// Converts a <c>Result</c> value to an <c>Option</c> value.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Ok</c> value.</typeparam>
    /// <typeparam name="TError">The type of the contained <c>Err</c> value.</typeparam>
    /// <returns>This <c>Result</c> value as a <c>Option</c> value.</returns>
    public static Option<TValue> ToOption<TValue, TError>(this Result<TValue, TError> self)
        where TValue : notnull
        where TError : notnull
    {
        return self.Match(Option<TValue>.Some, Option<TValue>.None);
    }

    /// <summary>
    /// Returns the contained <c>Ok</c> value, consuming the <c>self</c> value if it exists,
    /// otherwise returns the value provided by a function.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <param name="func">A function that provides a default value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Ok</c> value.</typeparam>
    /// <typeparam name="TError">The type of the contained <c>Err</c> value.</typeparam>
    /// <returns>The contained <c>Ok</c> or default value.</returns>
    public static TValue Default<TValue, TError>(this Result<TValue, TError> self, Func<TError, TValue> func)
        where TValue : notnull
        where TError : notnull
    {
        return self.IsOk ? self.Value : func(self.Error);
    }

    /// <summary>
    /// Returns the contained <c>Ok</c> value, consuming the <c>self</c> value if it exists,
    /// otherwise returns the specified default value.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <param name="value">The specified default value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Ok</c> value.</typeparam>
    /// <typeparam name="TError">The type of the contained <c>Err</c> value.</typeparam>
    /// <returns>The contained <c>Ok</c> or default value.</returns>
    public static TValue Default<TValue, TError>(this Result<TValue, TError> self, TValue value)
        where TValue : notnull
        where TError : notnull
    {
        return self.IsOk ? self.Value : value;
    }

    /// <summary>
    /// Transforms a <c>Result&lt;TValue, TError&gt;</c> into a <c>Result&lt;TOutput, TError&gt;</c>
    /// by applying a <c>mapping</c> function to the success value, leaving the error value untouched.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <param name="mapping">The function to apply to a contained value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The <c>Result&lt;TOutput, TError&gt;</c>.</returns>
    public static Result<TOutput, TError> Map<TValue, TError, TOutput>(this Result<TValue, TError> self, Func<TValue, TOutput> mapping)
        where TValue : notnull
        where TError : notnull
        where TOutput : notnull
    {
        return self.IsOk ? Result<TOutput, TError>.Ok(mapping(self.Value)) : Result<TOutput, TError>.Err(self.Error);
    }

    /// <summary>
    /// Transforms a <c>Result&lt;TValue, TError&gt;</c> into a <c>Result&lt;TValue, TOutput&gt;</c>
    /// by applying a <c>mapping</c> function to the error value, leaving the success value untouched.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <param name="mapping">The function to apply to a contained value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The <c>Result&lt;TValue, TOutput&gt;</c>.</returns>
    public static Result<TValue, TOutput> MapError<TValue, TError, TOutput>(this Result<TValue, TError> self, Func<TError, TOutput> mapping)
        where TValue : notnull
        where TError : notnull
        where TOutput : notnull
    {
        return self.IsErr ? Result<TValue, TOutput>.Err(mapping(self.Error)) : Result<TValue, TOutput>.Ok(self.Value);
    }

    /// <summary>
    /// Transforms a <c>Result&lt;TValue, TError&gt;</c> into a <c>Result&lt;TOutput, TError&gt;</c>
    /// by applying a <c>binder</c> function to the success value, leaving the error value untouched.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <param name="binder">The function to apply to the success value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The <c>Result&lt;TOutput, TError&gt;</c>.</returns>
    public static Result<TOutput, TError> Bind<TValue, TError, TOutput>(this Result<TValue, TError> self, Func<TValue, Result<TOutput, TError>> binder)
        where TValue : notnull
        where TError : notnull
        where TOutput : notnull
    {
        return self.IsOk ? binder(self.Value) : Result<TOutput, TError>.Err(self.Error);
    }

    /// <summary>
    /// Matches two states of a <c>Result&lt;TValue, TError&gt;</c>
    /// and applies an <c>ok</c> function to the success value, if the state is <c>Ok</c>,
    /// or applies an <c>error</c> function to the error value, if the state is <c>Err</c>.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <param name="ok">The function to apply to the success value.</param>
    /// <param name="error">The function to apply to the error value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The output value.</returns>
    public static TOutput Match<TValue, TError, TOutput>(this Result<TValue, TError> self, Func<TValue, TOutput> ok, Func<TError, TOutput> error)
        where TValue : notnull
        where TError : notnull
    {
        return self.IsOk ? ok(self.Value) : error(self.Error);
    }

    /// <summary>
    /// Matches two states of a <c>Result&lt;TValue, TError&gt;</c>
    /// and applies an <c>ok</c> function to the success value, if the state is <c>Ok</c>,
    /// or returns an <c>error</c> value, if the state is <c>Err</c>.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <param name="ok">The function to apply to the success value.</param>
    /// <param name="error">The output value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The output value.</returns>
    public static TOutput Match<TValue, TError, TOutput>(this Result<TValue, TError> self, Func<TValue, TOutput> ok, TOutput error)
        where TValue : notnull
        where TError : notnull
    {
        return self.IsOk ? ok(self.Value) : error;
    }

    /// <summary>
    /// Matches two states of a <c>Result&lt;TValue, TError&gt;</c>
    /// and applies an <c>ok</c> action to the success value, if the state is <c>Ok</c>,
    /// or applies an <c>error</c> action to the error value, if the state is <c>Err</c>.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <param name="ok">The action to apply to the success value.</param>
    /// <param name="error">The action to apply to the error value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    public static void Match<TValue, TError>(this Result<TValue, TError> self, Action<TValue> ok, Action<TError> error)
        where TValue : notnull
        where TError : notnull
    {
        if (self.IsOk)
        {
            ok(self.Value);
        }
        else
        {
            error(self.Error);
        }
    }

    /// <summary>
    /// The Identity function.
    /// </summary>
    /// <param name="self">The input value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <returns>The same value.</returns>
    public static Result<TValue, TError> Identity<TValue, TError>(this Result<TValue, TError> self)
        where TValue : notnull
        where TError : notnull
    {
        return self;
    }
}