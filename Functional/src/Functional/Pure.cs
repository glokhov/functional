namespace Functional;

public readonly record struct Pure<TValue>(TValue Value)
{
    public override string ToString()
    {
        return $"Pure({Value})";
    }
}