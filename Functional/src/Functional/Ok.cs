namespace Functional;

/// <summary>
/// Represents an OK or a Successful result.
/// </summary>
public sealed record Ok<T>(T Value) : Payload<T>(Value) where T : notnull
{
    /// <summary>
    /// Returns <c>Ok(Value)</c>.
    /// </summary>
    public override string ToString() => $"Ok({Value})";

    /// <summary>
    /// Returns the value of the <see cref="Ok{T}"/> instance.
    /// </summary>
    public static implicit operator Ok<T>(T value) => new(value);
}