namespace Functional;

/// <summary>
/// Static functions.
/// </summary>
public static class Prelude
{
    /// <summary>
    /// The Identity function.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns>The same value.</returns>
    public static TValue Identity<TValue>(TValue value)
    {
        return value;
    }

    /// <summary>
    /// Creates a <c>Some</c> value.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <typeparam name="TValue">The type of the input value.</typeparam>
    /// <returns>The <c>Pure</c> value representation of the input value.</returns>
    public static Pure<TValue> Some<TValue>(TValue value) where TValue : notnull => new(value);

    /// <summary>
    /// A <c>None</c> value.
    /// </summary>
    public static Fail<Unit> None => default;

    /// <summary>
    /// Creates an <c>Ok</c> value.
    /// </summary>
    /// <param name="value">The success value.</param>
    /// <typeparam name="TValue">The type of the success value.</typeparam>
    /// <returns>The <c>Pure</c> value representation of the success value.</returns>
    public static Pure<TValue> Ok<TValue>(TValue value) where TValue : notnull => new(value);

    /// <summary>
    /// Creates an <c>Err</c> value.
    /// </summary>
    /// <param name="error">The error value.</param>
    /// <typeparam name="TError">The type of the error value.</typeparam>
    /// <returns>The <c>Fail</c> value representation of the error value.</returns>
    public static Fail<TError> Err<TError>(TError error) where TError : notnull => new(error);
}