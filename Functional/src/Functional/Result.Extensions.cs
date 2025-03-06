namespace Functional;

public static class ResultExtensions
{
    public static Pure<TValue> AsPure<TValue, TError>(this Result<TValue, TError> result)
    {
        return (Pure<TValue>)result;
    }

    public static Fail<TError> AsFail<TValue, TError>(this Result<TValue, TError> result)
    {
        return (Fail<TError>)result;
    }
}