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
    private Result(TValue value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
        Error = default!;
        IsOk = true;
    }

    private Result(TError error)
    {
        Error = error ?? throw new ArgumentNullException(nameof(error));
        Value = default!;
        IsOk = false;
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

    internal TValue Value { get; }

    internal TError Error { get; }

    /// <summary>
    /// Returns <c>true</c> if the result is <c>Ok</c>.
    /// </summary>
    public bool IsOk { get; }

    /// <summary>
    /// Returns <c>true</c> if the result is <c>Err</c>.
    /// </summary>
    public bool IsErr => !IsOk;

    /// <summary>
    /// Returns the string representation of this instance.
    /// </summary>
    /// <returns>The string representation of this instance.</returns>
    public override string ToString()
    {
        return IsOk ? $"Ok({Value})" : $"Err({Error})";
    }

    /// <summary>
    /// Converts a <c>Pure</c> value to a <c>Result</c> value.
    /// </summary>
    /// <param name="pure">The <c>Pure</c> value.</param>
    /// <returns>The <c>Result</c> value.</returns>
    public static implicit operator Result<TValue, TError>(Pure<TValue> pure) => Ok(pure.Value);

    /// <summary>
    /// Converts a <c>Fail</c> value to a <c>Result</c> value.
    /// </summary>
    /// <param name="fail">The <c>Pure</c> value.</param>
    /// <returns>The <c>Result</c> value.</returns>
    public static implicit operator Result<TValue, TError>(Fail<TError> fail) => Err(fail.Error);

    /// <summary>
    /// Converts a <c>Result</c> value to a <c>Pure</c> value.
    /// </summary>
    /// <param name="result">The <c>Result</c> value.</param>
    /// <returns>The <c>Pure</c> value.</returns>
    /// <exception cref="InvalidCastException"><c>Result</c> value is <c>Err</c>.</exception>
    public static explicit operator Pure<TValue>(Result<TValue, TError> result)
    {
        return result.IsOk ? new Pure<TValue>(result.Value) : throw new InvalidCastException();
    }

    /// <summary>
    /// Converts a <c>Result</c> value to a <c>Fail</c> value.
    /// </summary>
    /// <param name="result">The <c>Result</c> value.</param>
    /// <returns>The <c>Fail</c> value.</returns>
    /// <exception cref="InvalidCastException"><c>Result</c> value is <c>Ok</c>.</exception>
    public static explicit operator Fail<TError>(Result<TValue, TError> result)
    {
        return result.IsErr ? new Fail<TError>(result.Error) : throw new InvalidCastException();
    }
}