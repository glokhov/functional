namespace Functional;

public static class ResultExtensions
{
    public static Pure<TValue> AsPure<TValue, TError>(this Result<TValue, TError> result) where TValue : notnull where TError : notnull
    {
        return (Pure<TValue>)result;
    }

    public static Fail<TError> AsFail<TValue, TError>(this Result<TValue, TError> result) where TValue : notnull where TError : notnull
    {
        return (Fail<TError>)result;
    }
}