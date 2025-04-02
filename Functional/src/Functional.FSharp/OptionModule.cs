using Microsoft.FSharp.Core;

namespace Functional.FSharp;

/// <summary>
/// Functions for the type <c>Option</c>.
/// </summary>
public static class OptionModule
{
    /// <summary>
    /// Returns the <c>self</c> value as a <c>FSharpOption</c> value.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Some</c> value.</typeparam>
    /// <returns>This <c>Option</c> value as a <c>FSharpOption</c> value.</returns>
    public static FSharpOption<TValue> ToFSharpOption<TValue>(this Option<TValue> self)
        where TValue : notnull
    {
        return self.Match(FSharpOption<TValue>.Some, FSharpOption<TValue>.None);
    }

    /// <summary>
    /// Returns the <c>self</c> value as a <c>FSharpValueOption</c> value.
    /// </summary>
    /// <param name="self">The <c>Option</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Some</c> value.</typeparam>
    /// <returns>This <c>Option</c> value as a <c>FSharpValueOption</c> value.</returns>
    public static FSharpValueOption<TValue> ToFSharpValueOption<TValue>(this Option<TValue> self)
        where TValue : notnull
    {
        return self.Match(FSharpValueOption<TValue>.Some, FSharpValueOption<TValue>.None);
    }
}