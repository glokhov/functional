namespace Functional;

/// <summary>
/// Provides conversion methods between values, options, and results.
/// </summary>
public static class Extensions
{
    extension<T>(T? value) where T : struct
    {
        /// <summary>
        /// Converts a value to an <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>
        /// An option containing the value if the value is not <c>null</c>, otherwise <c>None</c>.
        /// </returns>
        public Option<T> ToOption()
        {
            return value.HasValue ? Option<T>.Some(value.Value) : Option<T>.None;
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
            return value.HasValue ? Result<T, Unit>.Ok(value.Value) : Result<T, Unit>.Error(default);
        }
    }

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
        /// Converts an <see cref="Option{T}"/> to a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <returns>
        /// A result that is <c>Ok</c> with the value if the option is <c>Some</c>,
        /// otherwise an <c>Error</c> with <see cref="Unit"/>.
        /// </returns>
        public Result<T, Unit> ToResult()
        {
            return option is Some<T> some ? Result<T, Unit>.Ok(some.Value) : Result<T, Unit>.Error(default);
        }
    }

    extension<T, TError>(Result<T, TError> error) where T : notnull where TError : notnull
    {
        /// <summary>
        /// Converts a <see cref="Result{T, TError}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>
        /// An option containing the value if the result is <c>Ok</c>, otherwise <c>None</c>.
        /// </returns>
        public Option<T> ToOption()
        {
            return error is Ok<T> ok ? Option<T>.Some(ok.Value) : Option<T>.None;
        }
    }

    extension<T, TError>(Option<Result<T, TError>> option) where T : notnull where TError : notnull
    {
        /// <summary>
        /// Converts an <see cref="Option{T}"/> of a <see cref="Result{T, TError}"/>
        /// to a <see cref="Result{T, TError}"/> of <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>
        /// <c>Ok(Some(value))</c> for <c>Some(Ok(value))</c>, <c>Ok(None)</c> for <c>None</c>,
        /// and <c>Error(error)</c> for <c>Some(Error(error))</c>.
        /// </returns>
        public Result<Option<T>, TError> Transpose()
        {
            return option.Match(Some, None);

            Result<Option<T>, TError> Some(Result<T, TError> result)
            {
                return result.Match(Ok, Error);

                Result<Option<T>, TError> Ok(T value)
                {
                    return Result<Option<T>, TError>.Ok(Option<T>.Some(value));
                }

                Result<Option<T>, TError> Error(TError error)
                {
                    return Result<Option<T>, TError>.Error(error);
                }
            }

            Result<Option<T>, TError> None()
            {
                return Result<Option<T>, TError>.Ok(Option<T>.None);
            }
        }
    }

    extension<T, TError>(Result<Option<T>, TError> result) where T : notnull where TError : notnull
    {
        /// <summary>
        /// Converts a <see cref="Result{T, TError}"/> of an <see cref="Option{T}"/>
        /// to an <see cref="Option{T}"/> of a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <returns>
        /// <c>Some(Ok(value))</c> for <c>Ok(Some(value))</c>, <c>None</c> for <c>Ok(None)</c>,
        /// and <c>Some(Error(error))</c> for <c>Error(error)</c>.
        /// </returns>
        public Option<Result<T, TError>> Transpose()
        {
            return result.Match(Ok, Error);

            Option<Result<T, TError>> Ok(Option<T> option)
            {
                return option.Match(Some, None);

                Option<Result<T, TError>> Some(T value)
                {
                    return Option<Result<T, TError>>.Some(Result<T, TError>.Ok(value));
                }

                Option<Result<T, TError>> None()
                {
                    return Option<Result<T, TError>>.None;
                }
            }

            Option<Result<T, TError>> Error(TError error)
            {
                return Option<Result<T, TError>>.Some(Result<T, TError>.Error(error));
            }
        }
    }

    /// <summary>
    /// Applies the given function to each element of the sequence.
    /// Returns a sequence comprised of the results "x" for each element where the function returns <c>Some(x)</c>.
    /// </summary>
    /// <param name="source">The input sequence of type T.</param>
    /// <param name="chooser">A function to transform items of type T into options of type TOutput.</param>
    /// <returns>The result sequence.</returns>
    public static IEnumerable<TOutput> Choose<T, TOutput>(this IEnumerable<T> source, Func<T, Option<TOutput>> chooser)
        where T : notnull where TOutput : notnull
    {
        foreach (var option in source)
        {
            if (chooser(option) is Some<TOutput> some)
            {
                yield return some.Value;
            }
        }
    }

    /// <summary>
    /// Ignores the passed value.
    /// </summary>
    public static Unit Ignore<T>(this T _) => default;
}