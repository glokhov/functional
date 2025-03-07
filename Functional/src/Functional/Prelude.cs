namespace Functional;

public static class Prelude
{
    public static Pure<TValue> Some<TValue>(TValue value) where TValue : notnull => new(value);

    public static Fail<Unit> None => default;

    public static Pure<TValue> Ok<TValue>(TValue value) where TValue : notnull => new(value);

    public static Fail<TError> Err<TError>(TError error) where TError : notnull => new(error);
}