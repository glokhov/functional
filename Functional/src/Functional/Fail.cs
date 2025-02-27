namespace Functional;

public readonly record struct Fail<TError>(TError Error)
{
    public override string ToString()
    {
        return $"Fail({Error})";
    }
}