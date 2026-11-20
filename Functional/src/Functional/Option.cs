namespace Functional;

/// <summary>The type of optional values.</summary>
/// <remarks>
/// Use the constructors <see cref="Option{T}.Some(T)"/> and <see cref="Option{T}.None"/>
/// to create values of this type.
/// </remarks>
public union Option<T>(Some<T>, None) where T : notnull
{
    /// <summary>
    /// Do not use this constructor directly. Use <see cref="Option{T}.Some(T)"/>
    /// or <see cref="Option{T}.None"/> instead.
    /// </summary>
    public Option() : this(default(None))
    {
    }

    /// <summary>
    /// Creates the presence of a value.
    /// </summary>
    public static Option<T> Some(T value) => new Some<T>(value);

    /// <summary>
    /// Represents the absence of a value.
    /// </summary>
    public static readonly Option<T> None = default(None);

    /// <summary>
    /// Returns true if the option is Some.
    /// </summary>
    public bool IsSome => this is Some<T>;

    /// <summary>
    /// Returns true if the option is None.
    /// </summary>
    public bool IsNone => this is None;

    /// <summary>
    /// Binds the option to a function that returns another option.
    /// </summary>
    /// <param name="binder">A function to bind to.</param>
    /// <returns>An option of the output type of the binder.</returns>
    public Option<TOutput> Bind<TOutput>(Func<T, Option<TOutput>> binder) where TOutput : notnull
    {
        return this switch
        {
            Some<T> some => binder(some.Value),
            Functional.None => Option<TOutput>.None
        };
    }

    /// <summary>
    /// Maps the option to another option using a mapping function.
    /// </summary>
    /// <param name="mapping">A function to apply to the option value.</param>
    /// <returns>
    /// An option of the input value after applying the mapping function, or <c>None</c> if the input is <c>None</c>.
    /// </returns>
    public Option<TOutput> Map<TOutput>(Func<T, TOutput> mapping) where TOutput : notnull
    {
        return this switch
        {
            Some<T> some => Option<TOutput>.Some(mapping(some.Value)),
            Functional.None => Option<TOutput>.None
        };
    }

    /// <summary>
    /// Gets the value of the option if the option is <c>Some</c>, otherwise returns the specified default value.
    /// </summary>
    /// <param name="value">The specified default value.</param>
    /// <returns>The option if the option is <c>Some</c>, otherwise the default value.</returns>
    public T DefaultValue(T value)
    {
        return this switch
        {
            Some<T> some => some.Value,
            Functional.None => value
        };
    }

    /// <summary>
    /// Gets the value of the option if the option is <c>Some</c>,
    /// otherwise evaluates <paramref name="defThunk"/> and returns the result.
    /// </summary>
    /// <param name="defThunk">A thunk that provides a default value when evaluated.</param>
    /// <returns>
    /// The option if the option is <c>Some</c>, otherwise the result of evaluating <paramref name="defThunk"/>.
    /// </returns>
    public T DefaultWith(Func<T> defThunk)
    {
        return this switch
        {
            Some<T> some => some.Value,
            Functional.None => defThunk()
        };
    }

    /// <summary>
    /// Converts the option to a <see cref="Result{T, TError}"/>.
    /// </summary>
    /// <returns>
    /// A result that is <c>Ok</c> with the value if the option is <c>Some</c>,
    /// otherwise an <c>Error</c> with <see cref="Unit"/>.
    /// </returns>
    public Result<T, Unit> ToResult()
    {
        return this switch
        {
            Some<T> some => Result<T, Unit>.Ok(some.Value),
            Functional.None => Result<T, Unit>.Error(default)
        };
    }

    /// <summary>
    /// Converts a <see cref="Some{T}"/> to an <see cref="Option{T}"/>.
    /// </summary>
    public static implicit operator Option<T>(Some<T> some) => new(some);

    /// <summary>
    /// Converts a <see cref="None"/> to an <see cref="Option{T}"/>.
    /// </summary>
    public static implicit operator Option<T>(None none) => new(none);

    /// <summary>
    /// Converts a nullable value to an <see cref="Option{T}"/>.
    /// </summary>
    public static implicit operator Option<T>(T? value) => value is null ? None : Some(value);

    /// <summary>
    /// Converts an <see cref="Option{T}"/> to a nullable value.
    /// </summary>
    public static implicit operator T?(Option<T> option) => option is Some<T> some ? some.Value : default;

    /// <summary>
    /// Returns <c>Option(Value)</c> or <c>None</c>.
    /// </summary>
    public override string ToString() => $"{Value}";
}