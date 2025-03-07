using System.Diagnostics.CodeAnalysis;

namespace Functional;

/// <summary>
/// The type <c>Option</c> has two states,
/// <c>Some</c>, containing a value, and
/// <c>None</c>, containing no value.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public readonly record struct Option<TValue> where TValue : notnull
{
    private readonly TValue _value;
    private readonly bool _isSome;

    private Option(TValue value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
        _isSome = true;
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

    /// <summary>
    /// Returns <c>true</c> if the option is <c>Some</c>.
    /// </summary>
    [SuppressMessage("ReSharper", "ConvertToAutoPropertyWhenPossible")]
    public bool IsSome => _isSome;

    /// <summary>
    /// Returns <c>true</c> if the option is <c>None</c>.
    /// </summary>
    public bool IsNone => !_isSome;

    /// <summary>
    /// Transforms an <c>Option&lt;TValue&gt;</c> into an <c>Option&lt;TOutput&gt;</c>
    /// by applying a <c>mapping</c> function to the contained value,
    /// or into <c>None</c>, if there is no contained value.
    /// </summary>
    /// <param name="mapping">The function to apply to the contained value.</param>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The <c>Option&lt;TOutput&gt;</c> or <c>None</c>.</returns>
    public Option<TOutput> Map<TOutput>(Func<TValue, TOutput> mapping) where TOutput : notnull
    {
        return _isSome ? Option<TOutput>.Some(mapping(_value)) : Option<TOutput>.None;
    }

    /// <summary>
    /// Transforms an <c>Option&lt;TValue&gt;</c> into an <c>Option&lt;TOutput&gt;</c>
    /// by applying a <c>binder</c> function to the contained value,
    /// or into <c>None</c>, if there is no contained value.
    /// </summary>
    /// <param name="binder">The function to apply to the contained value.</param>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The <c>Option&lt;TOutput&gt;</c> or <c>None</c>.</returns>
    public Option<TOutput> Bind<TOutput>(Func<TValue, Option<TOutput>> binder) where TOutput : notnull
    {
        return _isSome ? binder(_value) : Option<TOutput>.None;
    }

    /// <summary>
    /// Matches two states of an <c>Option&lt;TValue&gt;</c>
    /// and applies a <c>some</c> function to the contained value, if the state is <c>Some</c>,
    /// or calls a <c>none</c> function, if the state is <c>None</c>.
    /// </summary>
    /// <param name="some">The function to apply to the contained value.</param>
    /// <param name="none">The function to call.</param>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The output value.</returns>
    public TOutput Match<TOutput>(Func<TValue, TOutput> some, Func<TOutput> none)
    {
        return _isSome ? some(_value) : none();
    }

    /// <summary>
    /// Matches two states of an <c>Option&lt;TValue&gt;</c>
    /// and applies a <c>some</c> function to the contained value, if the state is <c>Some</c>,
    /// or returns a <c>none</c> value, if the state is <c>None</c>.
    /// </summary>
    /// <param name="some">The function to apply to the contained value.</param>
    /// <param name="none">The output value.</param>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>The output value.</returns>
    public TOutput Match<TOutput>(Func<TValue, TOutput> some, TOutput none)
    {
        return _isSome ? some(_value) : none;
    }

    /// <summary>
    /// Matches two states of an <c>Option&lt;TValue&gt;</c>
    /// and applies a <c>some</c> action to the contained value, if the state is <c>Some</c>,
    /// or calls a <c>none</c> action, if the state is <c>None</c>.
    /// </summary>
    /// <param name="some">The action to apply to the contained value.</param>
    /// <param name="none">The action to call.</param>
    public void Match(Action<TValue> some, Action none)
    {
        if (_isSome)
        {
            some(_value);
        }
        else
        {
            none();
        }
    }

    /// <summary>
    /// Returns the string representation of this instance.
    /// </summary>
    /// <returns>The string representation of this instance.</returns>
    public override string ToString()
    {
        return _isSome ? $"Some({_value})" : "None";
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
    /// <exception cref="InvalidCastException"><c>Option</c> value equals <c>None</c>.</exception>
    public static explicit operator Pure<TValue>(Option<TValue> option)
    {
        return option.Match(some => new Pure<TValue>(some), () => throw new InvalidCastException());
    }
}