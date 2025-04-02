using Microsoft.FSharp.Core;

namespace Functional.FSharp;

/// <summary>
/// Extension methods for the type <c>FSharpOption</c>.
/// </summary>
public static class FSharpOptionExtensions
{
    /// <summary>
    /// Returns the <c>self</c> value as an <c>Option</c> value.
    /// </summary>
    /// <param name="self">The <c>FSharpOption</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Some</c> value.</typeparam>
    /// <returns>This <c>FSharpOption</c> value as a <c>Option</c> value.</returns>
    public static Option<TValue> ToOption<TValue>(this FSharpOption<TValue>? self)
        where TValue : notnull
    {
        return self != null ? Prelude.Some(self.Value) : Prelude.None;
    }
}