using Microsoft.FSharp.Core;

namespace Functional.FSharp;

/// <summary>
/// Extension methods for the type <c>FSharpResult</c>.
/// </summary>
public static class FSharpResultExtensions
{
    /// <summary>
    /// Returns the <c>self</c> value as a <c>Result</c> value.
    /// </summary>
    /// <param name="self">The <c>FSharpResult</c> value.</param>
    /// <typeparam name="TValue">The type of the contained <c>Ok</c> value.</typeparam>
    /// <typeparam name="TError">The type of the contained <c>Err</c> value.</typeparam>
    /// <returns>This <c>FSharpResult</c> value as a <c>Result</c> value.</returns>
    public static Result<TValue, TError> ToResult<TValue, TError>(this FSharpResult<TValue, TError> self)
        where TValue : notnull
        where TError : notnull
    {
        return self.IsOk ? Prelude.Ok(self.ResultValue) : Prelude.Err(self.ErrorValue);
    }
}