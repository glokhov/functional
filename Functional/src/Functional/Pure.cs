namespace Functional;

public readonly record struct Pure<TValue>(TValue Value) where TValue : notnull
{
    public override string ToString()
    {
        return $"Pure({Value})";
    }
}