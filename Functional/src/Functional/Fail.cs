namespace Functional;

/// <summary>
/// The type <c>Fail</c> represents an error value.
/// </summary>
/// <param name="Error">The error value.</param>
/// <typeparam name="TError">The type of the error value.</typeparam>
public readonly record struct Fail<TError>(TError Error) where TError : notnull
{
    public override string ToString()
    {
        return $"Fail({Error})";
    }
}