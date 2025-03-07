namespace Functional;

public static class ResultExtensions
{
    public static TValue Unwrap<TValue, TError>(this Result<TValue, TError> self) where TValue : notnull where TError : notnull
    {
        return self.AsPure().Value;
    }

    public static TError ExpectError<TValue, TError>(this Result<TValue, TError> self) where TValue : notnull where TError : notnull
    {
        return self.AsFail().Error;
    }

    public static Pure<TValue> AsPure<TValue, TError>(this Result<TValue, TError> self) where TValue : notnull where TError : notnull
    {
        return (Pure<TValue>)self;
    }

    public static Fail<TError> AsFail<TValue, TError>(this Result<TValue, TError> self) where TValue : notnull where TError : notnull
    {
        return (Fail<TError>)self;
    }
}