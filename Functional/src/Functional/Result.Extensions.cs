namespace Functional;

public static class ResultExtensions
{
    public static Pure<TValue> Pure<TValue, TError>(this Result<TValue, TError> result)
    {
        return (Pure<TValue>)result;
    }

    public static Fail<TError> Fail<TValue, TError>(this Result<TValue, TError> result)
    {
        return (Fail<TError>)result;
    }
}