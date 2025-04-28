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
    /// Returns the <c>Unit</c> value, consuming the <c>self</c> value.
    /// Because this function may throw an Exception, its use is generally discouraged.
    /// Instead, prefer to use the <c>Match</c> function.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Some</c> value.</typeparam>
    /// <returns>The <c>Unit</c> value.</returns>
    /// <exception cref="System.InvalidCastException">self value is <c>Some</c>.</exception>
    public static Unit ExpectUnit<TValue>(this Option<TValue> self) where TValue : notnull
    {
        return self.AsFail().Error;
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

    /// <summary>
    /// Returns the <c>self</c> value as the <c>Fail</c> value.
    /// Because this function may throw an Exception, its use is generally discouraged.
    /// Instead, prefer to use the <c>Match</c> function.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Some</c> value.</typeparam>
    /// <returns>This <c>Option</c> value as a <c>Fail</c> value.</returns>
    /// <exception cref="System.InvalidCastException">self value is <c>Some</c>.</exception>
    public static Fail<Unit> AsFail<TValue>(this Option<TValue> self) where TValue : notnull
    {
        return (Fail<Unit>)self;
    }

    /// <summary>
    /// Converts an <c>Option</c> value to a <c>Result</c> value.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <param name="error">The function to get the error value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <returns>This <c>Option</c> value as a <c>Result</c> value.</returns>
    public static Result<TValue, TError> ToResult<TValue, TError>(this Option<TValue> self, Func<TError> error)
        where TValue : notnull
        where TError : notnull
    {
        return self.Match(Result<TValue, TError>.Ok, () => Result<TValue, TError>.Err(error()));
    }

    /// <summary>
    /// Converts an <c>Option</c> value to a <c>Result</c> value.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <param name="error">The error value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <returns>This <c>Option</c> value as a <c>Result</c> value.</returns>
    public static Result<TValue, TError> ToResult<TValue, TError>(this Option<TValue> self, TError error)
        where TValue : notnull
        where TError : notnull
    {
        return self.Match(Result<TValue, TError>.Ok, Result<TValue, TError>.Err(error));
    }

    /// <summary>
    /// Returns the contained <c>Some</c> value, consuming the <c>self</c> value if it exists,
    /// otherwise returns the value provided by a function.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <param name="func">A function that provides a default value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Some</c> value.</typeparam>
    /// <returns>The contained <c>Some</c> or default value.</returns>
    public static TValue Default<TValue>(this Option<TValue> self, Func<TValue> func)
        where TValue : notnull
    {
        return self.IsSome ? self.Value : func();
    }

    /// <summary>
    /// Returns the contained <c>Some</c> value, consuming the <c>self</c> value if it exists,
    /// otherwise returns the specified default value.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <param name="value">The specified default value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Some</c> value.</typeparam>
    /// <returns>The contained <c>Some</c> or default value.</returns>
    public static TValue Default<TValue>(this Option<TValue> self, TValue value)
        where TValue : notnull
    {
        return self.IsSome ? self.Value : value;
    }

    /// <summary>
    /// Transforms an <c>Option&lt;TValue&gt;</c> into an <c>Option&lt;TOutput&gt;</c>
    /// by applying a <c>mapping</c> function to the contained value,
    /// or into <c>None</c>, if there is no contained value.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <param name="mapping">The function to apply to the contained value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The <c>Option&lt;TOutput&gt;</c> or <c>None</c>.</returns>
    public static Option<TOutput> Map<TValue, TOutput>(this Option<TValue> self, Func<TValue, TOutput> mapping)
        where TValue : notnull
        where TOutput : notnull
    {
        return self.IsSome ? Option<TOutput>.Some(mapping(self.Value)) : Option<TOutput>.None;
    }

    /// <summary>
    /// Transforms an <c>Option&lt;TValue&gt;</c> into an <c>Option&lt;TOutput&gt;</c>
    /// by applying a <c>binder</c> function to the contained value,
    /// or into <c>None</c>, if there is no contained value.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <param name="binder">The function to apply to the contained value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The <c>Option&lt;TOutput&gt;</c> or <c>None</c>.</returns>
    public static Option<TOutput> Bind<TValue, TOutput>(this Option<TValue> self, Func<TValue, Option<TOutput>> binder)
        where TValue : notnull
        where TOutput : notnull
    {
        return self.IsSome ? binder(self.Value) : Option<TOutput>.None;
    }

    /// <summary>
    /// Matches two states of an <c>Option&lt;TValue&gt;</c>
    /// and applies a <c>some</c> function to the contained value, if the state is <c>Some</c>,
    /// or calls a <c>none</c> function, if the state is <c>None</c>.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <param name="some">The function to apply to the contained value.</param>
    /// <param name="none">The function to call.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The output value.</returns>
    public static TOutput Match<TValue, TOutput>(this Option<TValue> self, Func<TValue, TOutput> some, Func<TOutput> none)
        where TValue : notnull
    {
        return self.IsSome ? some(self.Value) : none();
    }

    /// <summary>
    /// Matches two states of an <c>Option&lt;TValue&gt;</c>
    /// and applies a <c>some</c> function to the contained value, if the state is <c>Some</c>,
    /// or returns a <c>none</c> value, if the state is <c>None</c>.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <param name="some">The function to apply to the contained value.</param>
    /// <param name="none">The output value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The output value.</returns>
    public static TOutput Match<TValue, TOutput>(this Option<TValue> self, Func<TValue, TOutput> some, TOutput none)
        where TValue : notnull
    {
        return self.IsSome ? some(self.Value) : none;
    }

    /// <summary>
    /// Matches two states of an <c>Option&lt;TValue&gt;</c>
    /// and applies a <c>some</c> action to the contained value, if the state is <c>Some</c>,
    /// or calls a <c>none</c> action, if the state is <c>None</c>.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <param name="some">The action to apply to the contained value.</param>
    /// <param name="none">The action to call.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public static void Match<TValue>(this Option<TValue> self, Action<TValue> some, Action none)
        where TValue : notnull
    {
        if (self.IsSome)
        {
            some(self.Value);
        }
        else
        {
            none();
        }
    }
}