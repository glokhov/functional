namespace Functional;

/// <summary>
/// Provides conversion methods between values, options, and results.
/// </summary>
public static class Extensions
{
    extension<T>(T? value) where T : notnull
    {
        /// <summary>
        /// Converts a value to an <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>
        /// An option containing the value if the value is not <c>null</c>, otherwise <c>None</c>.
        /// </returns>
        public Option<T> ToOption()
        {
            return value is null ? Option<T>.None : Option<T>.Some(value);
        }

        /// <summary>
        /// Converts a value to a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <returns>
        /// A result that is <c>Ok</c> with the value if the value is not <c>null</c>,
        /// otherwise an <c>Error</c> with <see cref="Unit"/>.
        /// </returns>
        public Result<T, Unit> ToResult()
        {
            return value is null ? Result<T, Unit>.Error(default) : Result<T, Unit>.Ok(value);
        }
    }

    extension<T>(Option<T> option) where T : notnull
    {
        /// <summary>
        /// Converts an option to a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <returns>
        /// A result that is <c>Ok</c> with the value if the option is <c>Some</c>,
        /// otherwise an <c>Error</c> with <see cref="Unit"/>.
        /// </returns>
        public Result<T, Unit> ToResult()
        {
            return option switch
            {
                None => Result<T, Unit>.Error(default),
                Some<T> some => Result<T, Unit>.Ok(some.Value)
            };
        }
    }

    extension<T, TError>(Result<T, TError> error) where T : notnull where TError : notnull
    {
        /// <summary>
        /// Converts a result to an <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>
        /// An option containing the value if the result is <c>Ok</c>, otherwise <c>None</c>.
        /// </returns>
        public Option<T> ToOption()
        {
            return error switch
            {
                Error<TError> => Option<T>.None,
                Ok<T> ok => Option<T>.Some(ok.Value)
            };
        }
    }
}