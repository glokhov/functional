using Microsoft.FSharp.Core;
using static Functional.Prelude;

namespace Functional.FSharp;

/// <summary>
/// Extension methods for the type <c>FSharpValueOption</c>.
/// </summary>
public static class FSharpValueOptionExtensions
{
    /// <summary>
    /// Returns the <c>self</c> value as an <c>Option</c> value.
    /// </summary>
    /// <param name="self">The <c>FSharpValueOption</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Some</c> value.</typeparam>
    /// <returns>This <c>FSharpValueOption</c> value as a <c>Option</c> value.</returns>
    public static Option<TValue> ToOption<TValue>(this FSharpValueOption<TValue> self)
        where TValue : notnull
    {
        return self.IsSome ? Option<TValue>.Some(self.Value) : Option<TValue>.None;
    }
}