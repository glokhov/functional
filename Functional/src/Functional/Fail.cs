namespace Functional;

public readonly record struct Fail<TError>(TError Error) where TError : notnull
{
    public override string ToString()
    {
        return $"Fail({Error})";
    }
}