using System.Diagnostics.CodeAnalysis;

namespace Functional;

public readonly record struct Result<TValue, TError> where TValue : notnull where TError : notnull
{
    private readonly TValue _value;
    private readonly TError _error;
    private readonly bool _isOk;

    private Result(TValue value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
        _error = default!;
        _isOk = true;
    }

    private Result(TError error)
    {
        _error = error ?? throw new ArgumentNullException(nameof(error));
        _value = default!;
        _isOk = false;
    }

    public static Result<TValue, TError> Ok(TValue value)
    {
        return new Result<TValue, TError>(value);
    }

    public static Result<TValue, TError> Err(TError error)
    {
        return new Result<TValue, TError>(error);
    }

    [SuppressMessage("ReSharper", "ConvertToAutoPropertyWhenPossible")]
    public bool IsOk => _isOk;

    public bool IsErr => !_isOk;

    public Result<TResult, TError> Map<TResult>(Func<TValue, TResult> func) where TResult : notnull
    {
        return _isOk ? Result<TResult, TError>.Ok(func(_value)) : Result<TResult, TError>.Err(_error);
    }

    public Result<TResult, TError> Bind<TResult>(Func<TValue, Result<TResult, TError>> func) where TResult : notnull
    {
        return _isOk ? func(_value) : Result<TResult, TError>.Err(_error);
    }

    public TResult Match<TResult>(Func<TValue, TResult> ok, Func<TError, TResult> error)
    {
        return _isOk ? ok(_value) : error(_error);
    }

    public TResult Match<TResult>(Func<TValue, TResult> ok, TResult error)
    {
        return _isOk ? ok(_value) : error;
    }

    public void Match(Action<TValue> ok, Action<TError> error)
    {
        if (_isOk)
        {
            ok(_value);
        }
        else
        {
            error(_error);
        }
    }

    public override string ToString()
    {
        return _isOk ? $"Ok({_value})" : $"Err({_error})";
    }

    public static implicit operator Result<TValue, TError>(Pure<TValue> pure) => Ok(pure.Value);

    public static implicit operator Result<TValue, TError>(Fail<TError> fail) => Err(fail.Error);

    public static explicit operator Pure<TValue>(Result<TValue, TError> result) => result.Match(ok => new Pure<TValue>(ok), _ => throw new InvalidCastException());

    public static explicit operator Fail<TError>(Result<TValue, TError> result) => result.Match(_ => throw new InvalidCastException(), error => new Fail<TError>(error));
}