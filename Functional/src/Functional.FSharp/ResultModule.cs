using Microsoft.FSharp.Core;

namespace Functional.FSharp;

/// <summary>
/// Functions for the type <c>Result</c>.
/// </summary>
public static class ResultModule
{
    /// <summary>
    /// Returns the <c>self</c> value as a <c>FSharpResult</c> value.
    /// </summary>
    /// <param name="self">The <c>Result</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Ok</c> value.</typeparam>
    /// <typeparam name="TError">The type of the contained <c>Err</c> value.</typeparam>
    /// <returns>This <c>Result</c> value as a <c>FSharpResult</c> value.</returns>
    public static FSharpResult<TValue, TError> ToFSharpResult<TValue, TError>(this Result<TValue, TError> self)
        where TValue : notnull
        where TError : notnull
    {
        return self.Match(FSharpResult<TValue, TError>.NewOk, FSharpResult<TValue, TError>.NewError);
    }
}