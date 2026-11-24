namespace Functional;

/// <summary>The type of optional values.</summary>
public union Option<T>(Some<T>, None) where T : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Option{T}"/> struct with default values.
    /// </summary>
    [Obsolete("Use Some<T> or None instead.", true)]
    public Option() : this(default(None))
    {
    }

    /// <summary>
    /// Binds the option to a function that returns another option.
    /// </summary>
    /// <param name="binder">A function to bind to.</param>
    /// <returns>An option of the output type of the binder.</returns>
    public Option<TOutput> Bind<TOutput>(Func<T, Option<TOutput>> binder) where TOutput : notnull
    {
        return this is Some<T> some ? binder(some.Value) : default(None);
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
        return this is Some<T> some ? new Some<TOutput>(mapping(some.Value)) : default(None);
    }

    /// <summary>
    /// Matches the option to a function that handles the <c>Some</c> case and a function that handles the <c>None</c> case.
    /// </summary>
    /// <param name="someAction">A function to handle the <c>Some</c> case.</param>
    /// <param name="noneAction">A function to handle the <c>None</c> case.</param>
    public void Match(Action<T> someAction, Action noneAction)
    {
        switch (this)
        {
            case Some<T> some:
                someAction(some.Value);
                break;
            case None:
                noneAction();
                break;
        }
    }

    /// <summary>
    /// Matches the option to a function that handles the <c>Some</c> case and a function that handles the <c>None</c> case.
    /// </summary>
    /// <param name="someFunc">A function to handle the <c>Some</c> case.</param>
    /// <param name="noneFunc">A function to handle the <c>None</c> case.</param>
    /// <returns>The result of applying the appropriate function based on the option's state.</returns>
    public TOutput Match<TOutput>(Func<T, TOutput> someFunc, Func<TOutput> noneFunc)
    {
        return this is Some<T> some ? someFunc(some.Value) : noneFunc();
    }

    /// <summary>
    /// Matches the option to a function that handles the <c>Some</c> case and a value that handles the <c>None</c> case.
    /// </summary>
    /// <param name="someFunc">A function to handle the <c>Some</c> case.</param>
    /// <param name="noneValue">A value to handle the <c>None</c> case.</param>
    /// <returns>The result of applying the appropriate function based on the option's state.</returns>
    public TOutput Match<TOutput>(Func<T, TOutput> someFunc, TOutput noneValue)
    {
        return this is Some<T> some ? someFunc(some.Value) : noneValue;
    }

    /// <summary>
    /// Gets the value of the option if the option is <c>Some</c>,
    /// otherwise evaluates <paramref name="defaultFunc"/> and returns the result.
    /// </summary>
    /// <param name="defaultFunc">A function that provides a default value when evaluated.</param>
    /// <returns>
    /// The option if the option is <c>Some</c>, otherwise the result of evaluating <paramref name="defaultFunc"/>.
    /// </returns>
    public T Default(Func<T> defaultFunc)
    {
        return this is Some<T> some ? some.Value : defaultFunc();
    }

    /// <summary>
    /// Gets the value of the option if the option is <c>Some</c>, otherwise returns the specified default value.
    /// </summary>
    /// <param name="defaultValue">The specified default value.</param>
    /// <returns>The option if the option is <c>Some</c>, otherwise the default value.</returns>
    public T Default(T defaultValue)
    {
        return this is Some<T> some ? some.Value : defaultValue;
    }

    /// <summary>
    /// Converts a nullable value to an <see cref="Option{T}"/>.
    /// </summary>
    public static implicit operator Option<T>(T? value) => value is null ? default(None) : new Some<T>(value);

    /// <summary>
    /// Converts an <see cref="Option{T}"/> to a nullable value.
    /// </summary>
    public static implicit operator T?(Option<T> option) => option is Some<T> some ? some.Value : default;

    /// <summary>
    /// Returns <c>Option(Value)</c> or <c>None</c>.
    /// </summary>
    public override string ToString() => $"{Value}";
}