using System.Diagnostics.CodeAnalysis;

namespace Functional;

public readonly record struct Option<TValue>
{
    private readonly TValue _value;
    private readonly bool _isSome;

    private Option(TValue value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
        _isSome = true;
    }

    public static Option<TValue> Some(TValue value)
    {
        return new Option<TValue>(value);
    }

    public static readonly Option<TValue> None = default;

    [SuppressMessage("ReSharper", "ConvertToAutoPropertyWhenPossible")]
    public bool IsSome => _isSome;

    public bool IsNone => !_isSome;

    public Option<TResult> Map<TResult>(Func<TValue, TResult> func)
    {
        return _isSome ? Option<TResult>.Some(func(_value)) : Option<TResult>.None;
    }

    public Option<TResult> Bind<TResult>(Func<TValue, Option<TResult>> func)
    {
        return _isSome ? func(_value) : Option<TResult>.None;
    }

    public TResult Match<TResult>(Func<TValue, TResult> some, Func<TResult> none)
    {
        return _isSome ? some(_value) : none();
    }

    public TResult Match<TResult>(Func<TValue, TResult> some, TResult none)
    {
        return _isSome ? some(_value) : none;
    }

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

    public override string ToString()
    {
        return _isSome ? $"Some({_value})" : "None";
    }

    public static implicit operator Option<TValue>(Pure<TValue> pure) => Some(pure.Value);

    public static implicit operator Option<TValue>(Fail<Unit> _) => default;
}