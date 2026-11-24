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
            return value.HasValue ? new Some<T>(value.Value) : default(None);
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
            return value.HasValue ? new Ok<T>(value.Value) : new Error<Unit>(default(Unit));
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
            return value is null ? default(None) : new Some<T>(value);
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
            return value is null ? new Error<Unit>(default) : new Ok<T>(value);
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
            return option is Some<T> some ? new Ok<T>(some.Value) : new Error<Unit>(default);
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
            return error is Ok<T> ok ? new Some<T>(ok.Value) : default(None);
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
            return option.Match(HandleSome, HandleNone);

            Result<Option<T>, TError> HandleSome(Result<T, TError> result)
            {
                return result.Match(HandleOk, HandleError);

                Result<Option<T>, TError> HandleOk(T value)
                {
                    return new Ok<Option<T>>(new Some<T>(value));
                }

                Result<Option<T>, TError> HandleError(TError error)
                {
                    return new Error<TError>(error);
                }
            }

            Result<Option<T>, TError> HandleNone()
            {
                return new Ok<Option<T>>(default(None));
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
            return result.Match(HandleOk, HandleError);

            Option<Result<T, TError>> HandleOk(Option<T> option)
            {
                return option.Match(HandleSome, HandleNone);

                Option<Result<T, TError>> HandleSome(T value)
                {
                    return new Some<Result<T, TError>>(new Ok<T>(value));
                }

                Option<Result<T, TError>> HandleNone()
                {
                    return default(None);
                }
            }

            Option<Result<T, TError>> HandleError(TError error)
            {
                return new Some<Result<T, TError>>(new Error<TError>(error));
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