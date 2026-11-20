namespace Functional;

/// <summary>
/// Represents a value.
/// </summary>
public closed record Payload<T>(T Value) where T : notnull
{
    /// <summary>
    /// The value.
    /// </summary>
    public T Value { get; } = Value ?? throw new ArgumentNullException(nameof(Value));

    /// <summary>
    /// Returns the value of the <see cref="Payload{T}"/> instance.
    /// </summary>
    public static implicit operator T(Payload<T> value) => value.Value;
}