using System.Diagnostics.CodeAnalysis;

namespace Functional;

/// <summary>
/// The type <c>Result</c> has two states,
/// <c>Ok</c>, representing a success and containing a success value, and
/// <c>Err</c>, representing an error and containing an error value.
/// </summary>
/// <typeparam name="TValue">The type of the success value.</typeparam>
/// <typeparam name="TError">The type of the error value.</typeparam>
public readonly record struct Result<TValue, TError> where TValue : notnull where TError : notnull
{
    private readonly TValue _value;
    private readonly TError _error;
    private readonly bool _isOk;

    private Result(TValue value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
        _error = default!;
        _isOk = true;
    }

    private Result(TError error)
    {
        _error = error ?? throw new ArgumentNullException(nameof(error));
        _value = default!;
        _isOk = false;
    }

    /// <summary>
    /// Creates an <c>Ok</c> value.
    /// </summary>
    /// <param name="value">The success value.</param>
    /// <returns>The <c>Result</c> representation of the success value.</returns>
    public static Result<TValue, TError> Ok(TValue value)
    {
        return new Result<TValue, TError>(value);
    }

    /// <summary>
    /// Creates an <c>Err</c> value.
    /// </summary>
    /// <param name="error">The error value.</param>
    /// <returns>The <c>Result</c> representation of the error value.</returns>
    public static Result<TValue, TError> Err(TError error)
    {
        return new Result<TValue, TError>(error);
    }

    /// <summary>
    /// Returns <c>true</c> if the result is <c>Ok</c>.
    /// </summary>
    [SuppressMessage("ReSharper", "ConvertToAutoPropertyWhenPossible")]
    public bool IsOk => _isOk;

    /// <summary>
    /// Returns <c>true</c> if the result is <c>Err</c>.
    /// </summary>
    public bool IsErr => !_isOk;

    /// <summary>
    /// Transforms a <c>Result&lt;TValue, TError&gt;</c> into a <c>Result&lt;TOutput, TError&gt;</c>
    /// by applying a <c>mapping</c> function to the success value, leaving the error value untouched.
    /// </summary>
    /// <param name="mapping">The function to apply to a contained value.</param>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The <c>Result&lt;TOutput, TError&gt;</c>.</returns>
    public Result<TOutput, TError> Map<TOutput>(Func<TValue, TOutput> mapping) where TOutput : notnull
    {
        return _isOk ? Result<TOutput, TError>.Ok(mapping(_value)) : Result<TOutput, TError>.Err(_error);
    }

    /// <summary>
    /// Transforms a <c>Result&lt;TValue, TError&gt;</c> into a <c>Result&lt;TOutput, TError&gt;</c>
    /// by applying a <c>binder</c> function to the success value, leaving the error value untouched.
    /// </summary>
    /// <param name="binder">The function to apply to the success value.</param>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The <c>Result&lt;TOutput, TError&gt;</c>.</returns>
    public Result<TOutput, TError> Bind<TOutput>(Func<TValue, Result<TOutput, TError>> binder) where TOutput : notnull
    {
        return _isOk ? binder(_value) : Result<TOutput, TError>.Err(_error);
    }

    /// <summary>
    /// Matches two states of a <c>Result&lt;TValue, TError&gt;</c>
    /// and applies an <c>ok</c> function to the success value, if the state is <c>Ok</c>,
    /// or applies an <c>error</c> function to the error value, if the state is <c>Err</c>.
    /// </summary>
    /// <param name="ok">The function to apply to the success value.</param>
    /// <param name="error">The function to apply to the error value.</param>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The output value.</returns>
    public TOutput Match<TOutput>(Func<TValue, TOutput> ok, Func<TError, TOutput> error)
    {
        return _isOk ? ok(_value) : error(_error);
    }

    /// <summary>
    /// Matches two states of a <c>Result&lt;TValue, TError&gt;</c>
    /// and applies an <c>ok</c> function to the success value, if the state is <c>Ok</c>,
    /// or returns an <c>error</c> value, if the state is <c>Err</c>.
    /// </summary>
    /// <param name="ok">The function to apply to the success value.</param>
    /// <param name="error">The output value.</param>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The output value.</returns>
    public TOutput Match<TOutput>(Func<TValue, TOutput> ok, TOutput error)
    {
        return _isOk ? ok(_value) : error;
    }

    /// <summary>
    /// Matches two states of a <c>Result&lt;TValue, TError&gt;</c>
    /// and applies an <c>ok</c> action to the success value, if the state is <c>Ok</c>,
    /// or applies an <c>error</c> action to the error value, if the state is <c>Err</c>.
    /// </summary>
    /// <param name="ok">The action to apply to the success value.</param>
    /// <param name="error">The action to apply to the error value.</param>
    public void Match(Action<TValue> ok, Action<TError> error)
    {
        if (_isOk)
        {
            ok(_value);
        }
        else
        {
            error(_error);
        }
    }

    public override string ToString()
    {
        return _isOk ? $"Ok({_value})" : $"Err({_error})";
    }

    public static implicit operator Result<TValue, TError>(Pure<TValue> pure) => Ok(pure.Value);

    public static implicit operator Result<TValue, TError>(Fail<TError> fail) => Err(fail.Error);

    public static explicit operator Pure<TValue>(Result<TValue, TError> result) => result.Match(ok => new Pure<TValue>(ok), _ => throw new InvalidCastException());

    public static explicit operator Fail<TError>(Result<TValue, TError> result) => result.Match(_ => throw new InvalidCastException(), error => new Fail<TError>(error));
}