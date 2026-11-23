namespace Functional;

/// <summary>
/// Provides a set of functions and values for creating functional types such as <see cref="Some{T}"/>,
/// <see cref="None"/> (<see cref="Nothing"/>), <see cref="Ok{T}"/>, and <see cref="Error{T}"/>.
/// </summary>
public static class Prelude
{
    /// <summary>
    /// Creates a value.
    /// </summary>
    public static Some<T> Some<T>(T value) where T : notnull => new(value);

    /// <summary>
    /// Represents the absence of a value.
    /// </summary>
    public static None Nothing => default;

    /// <summary>
    /// Creates an OK or a Successful result.
    /// </summary>
    public static Ok<T> Ok<T>(T value) where T : notnull => new(value);

    /// <summary>
    /// Creates an Error or a Failure result.
    /// </summary>
    public static Error<T> Error<T>(T value) where T : notnull => new(value);

    /// <summary>
    /// The Identity function.
    /// </summary>
    public static T Identity<T>(T value) => value;
}