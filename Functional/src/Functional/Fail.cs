namespace Functional;

/// <summary>
/// The type <c>Fail</c> represents an error value.
/// </summary>
/// <param name="Error">The error value.</param>
/// <typeparam name="TError">The type of the error value.</typeparam>
public readonly record struct Fail<TError>(TError Error) where TError : notnull
{
    /// <summary>
    /// Returns the string representation of this instance.
    /// </summary>
    /// <returns>The string representation of this instance.</returns>
    public override string ToString()
    {
        return $"Fail({Error})";
    }
}