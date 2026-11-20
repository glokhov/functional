namespace Functional;

/// <summary>
/// Represents the presence of a value.
/// </summary>
public sealed record Some<T>(T Value) : Payload<T>(Value) where T : notnull
{
    /// <summary>
    /// Returns <c>Some(Value)</c>.
    /// </summary>
    public override string ToString() => $"Some({Value})";

    /// <summary>
    /// Returns the value of the <see cref="Some{T}"/> instance.
    /// </summary>
    public static implicit operator Some<T>(T value) => new(value);
}