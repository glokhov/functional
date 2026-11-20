namespace Functional;

/// <summary>
/// Represents an Error or a Failure.
/// </summary>
public sealed record Error<T>(T Value) : Payload<T>(Value) where T : notnull
{
    /// <summary>
    /// Returns <c>Error(Value)</c>.
    /// </summary>
    public override string ToString() => $"Error({Value})";

    /// <summary>
    /// Returns the value of the <see cref="Error{T}"/> instance.
    /// </summary>
    public static implicit operator Error<T>(T value) => new(value);
}