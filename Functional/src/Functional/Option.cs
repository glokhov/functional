namespace Functional;

/// <summary>
/// The type <c>Option</c> has two states,
/// <c>Some</c>, containing a value, and
/// <c>None</c>, containing no value.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public readonly record struct Option<TValue> where TValue : notnull
{
    private Option(TValue value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
        IsSome = true;
    }

    /// <summary>
    /// Creates a <c>Some</c> value.
    /// </summary>
    /// <param name="value">The input value.</param>
    /// <returns>The <c>Option</c> representation of the input value.</returns>
    public static Option<TValue> Some(TValue value)
    {
        return new Option<TValue>(value);
    }

    /// <summary>
    /// The <c>None</c> value.
    /// </summary>
    public static readonly Option<TValue> None = default;

    internal TValue Value { get; }

    /// <summary>
    /// Returns <c>true</c> if the option is <c>Some</c>.
    /// </summary>
    public bool IsSome { get; }

    /// <summary>
    /// Returns <c>true</c> if the option is <c>None</c>.
    /// </summary>
    public bool IsNone => !IsSome;

    /// <summary>
    /// Returns the string representation of this instance.
    /// </summary>
    /// <returns>The string representation of this instance.</returns>
    public override string ToString()
    {
        return IsSome ? $"Some({Value})" : "None";
    }

    /// <summary>
    /// Converts a <c>Pure</c> value to an <c>Option</c> value.
    /// </summary>
    /// <param name="pure">The <c>Pure</c> value.</param>
    /// <returns>The <c>Option</c> value.</returns>
    public static implicit operator Option<TValue>(Pure<TValue> pure) => Some(pure.Value);

    /// <summary>
    /// Converts a <c>Fail</c> value to <c>None</c>.
    /// </summary>
    /// <param name="fail">The <c>Fail</c> value.</param>
    /// <returns><c>None</c>.</returns>
    public static implicit operator Option<TValue>(Fail<Unit> fail) => default;

    /// <summary>
    /// Converts an <c>Option</c> value to a <c>Pure</c> value.
    /// </summary>
    /// <param name="option">The <c>Option</c> value.</param>
    /// <returns>The <c>Pure</c> value.</returns>
    /// <exception cref="InvalidCastException"><c>Option</c> value is <c>None</c>.</exception>
    public static explicit operator Pure<TValue>(Option<TValue> option)
    {
        return option.IsSome ? new Pure<TValue>(option.Value) : throw new InvalidCastException();
    }

    /// <summary>
    /// Converts an <c>Option</c> value to a <c>Fail</c> value.
    /// </summary>
    /// <param name="option">The <c>Option</c> value.</param>
    /// <returns>The <c>Fail</c> value.</returns>
    /// <exception cref="InvalidCastException"><c>Option</c> value is <c>Some</c>.</exception>
    public static explicit operator Fail<Unit>(Option<TValue> option)
    {
        return option.IsNone ? default : throw new InvalidCastException();
    }
}