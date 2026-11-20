namespace Functional;

/// <summary>
/// Helper type for error handling without exceptions.
/// </summary>
public union Result<T, TError>(Ok<T>, Error<TError>) where T : notnull where TError : notnull
{
    /// <summary>
    /// Do not use this constructor directly. Use <see cref="Result{T, TError}.Ok(T)"/>
    /// or <see cref="Result{T, TError}.Error(TError)"/> instead.
    /// </summary>
    public Result() : this(new Error<TError>(default!))
    {
    }

    /// <summary>
    /// Creates an OK or a Successful result.
    /// </summary>
    public static Result<T, TError> Ok(T value) => new Ok<T>(value);

    /// <summary>
    /// Creates an Error or a Failure.
    /// </summary>
    public static Result<T, TError> Error(TError value) => new Error<TError>(value);

    /// <summary>
    /// Returns true if the result is OK.
    /// </summary>
    public bool IsOk => this is Ok<T>;

    /// <summary>
    /// Returns true if the result is an Error.
    /// </summary>
    public bool IsError => this is Error<TError>;

    /// <summary>
    /// Binds the result to a function that returns another result.
    /// </summary>
    /// <param name="binder">A function to bind to.</param>
    public Result<TOutput, TError> Bind<TOutput>(Func<T, Result<TOutput, TError>> binder) where TOutput : notnull
    {
        return this switch
        {
            Ok<T> ok => binder(ok.Value),
            Error<TError> error => Result<TOutput, TError>.Error(error.Value)
        };
    }

    /// <summary>
    /// Maps the result to another result using a mapping function.
    /// </summary>
    /// <param name="mapping">A function to apply to the OK result value.</param>
    /// <returns>
    /// A result of the input value after applying the mapping function, or <c>Error</c> if the input is <c>Error</c>.
    /// </returns>
    public Result<TOutput, TError> Map<TOutput>(Func<T, TOutput> mapping) where TOutput : notnull
    {
        return this switch
        {
            Ok<T> ok => Result<TOutput, TError>.Ok(mapping(ok.Value)),
            Error<TError> error => Result<TOutput, TError>.Error(error.Value)
        };
    }

    /// <summary>
    /// Maps the error of the result to another error using a mapping function.
    /// </summary>
    /// <param name="mapping">A function to apply to the Error value.</param>
    /// <returns>
    /// A result of the input value after applying the mapping function, or <c>Ok</c> if the input is <c>Ok</c>.
    /// </returns>
    public Result<T, TOutput> MapError<TOutput>(Func<TError, TOutput> mapping) where TOutput : notnull
    {
        return this switch
        {
            Ok<T> ok => Result<T, TOutput>.Ok(ok.Value),
            Error<TError> error => Result<T, TOutput>.Error(mapping(error.Value))
        };
    }

    /// <summary>
    /// Gets the value of the result if the result is <c>Ok</c>,
    /// otherwise returns the specified default value.
    /// </summary>
    /// <param name="value">The specified default value.</param>
    /// <returns>
    /// The result value if the result is <c>Ok</c>, otherwise the default value.
    /// </returns>
    public T DefaultValue(T value)
    {
        return this switch
        {
            Ok<T> ok => ok.Value,
            Error<TError> => value
        };
    }

    /// <summary>
    /// Gets the value of the result if the result is <c>Ok</c>,
    /// otherwise evaluates <paramref name="defThunk"/> and returns the result.
    /// </summary>
    /// <param name="defThunk">A function that provides a default value when evaluated.</param>
    /// <returns>
    /// The result value if the result is <c>Ok</c>,
    /// otherwise the result of evaluating <paramref name="defThunk"/>.
    /// </returns>
    public T DefaultWith(Func<T> defThunk)
    {
        return this switch
        {
            Ok<T> ok => ok.Value,
            Error<TError> => defThunk()
        };
    }

    /// <summary>
    /// Converts the result to an <see cref="Option{T}"/>.
    /// </summary>
    /// <returns>
    /// An option containing the value if the result is <c>Ok</c>, otherwise <c>None</c>.
    /// </returns>
    public Option<T> ToOption()
    {
        return this switch
        {
            Ok<T> ok => Option<T>.Some(ok.Value),
            Error<TError> => Option<T>.None
        };
    }

    /// <summary>
    /// Converts an <see cref="Ok{T}"/> to a <see cref="Result{T, TError}"/>.
    /// </summary>
    public static implicit operator Result<T, TError>(Ok<T> ok) => new(ok);

    /// <summary>
    /// Converts an <see cref="Error{TError}"/> to a <see cref="Result{T, TError}"/>.
    /// </summary>
    public static implicit operator Result<T, TError>(Error<TError> error) => new(error);

    /// <summary>
    /// Returns <c>Ok(Value)</c> or <c>Error(Value)</c>.
    /// </summary>
    public override string ToString() => $"{Value}";
}