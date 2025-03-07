namespace Functional;

/// <summary>
/// The type <c>Pure</c> represents a success value.
/// </summary>
/// <param name="Value">The success value.</param>
/// <typeparam name="TValue">The type of the success value.</typeparam>
public readonly record struct Pure<TValue>(TValue Value) where TValue : notnull
{
    public override string ToString()
    {
        return $"Pure({Value})";
    }
}