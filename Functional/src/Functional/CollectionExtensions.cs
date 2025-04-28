namespace Functional;

/// <summary>
/// Extensions for collection types.
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Applies a function to each element in a sequence and then returns a sequence of values where the applied function
    /// returned Some(value). Returns an empty sequence when the input sequence is empty or when the applied chooser function
    /// returns None for all elements.
    /// </summary>
    /// <param name="source">The input <c>IEnumerable&lt;TValue&gt;</c>.</param>
    /// <param name="chooser">The function to be applied to the sequence elements.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>
    /// The resulting <c>IEnumerable&lt;TOutput&gt;</c> comprising the values where the chooser function returned Some(value).
    /// </returns>
    public static IEnumerable<TOutput> Choose<TValue, TOutput>(this IEnumerable<TValue> source, Func<TValue, Option<TOutput>> chooser)
        where TValue : notnull
        where TOutput : notnull
    {
        return source.Select(chooser).Where(item => item.IsSome).Select(item => item.Value);
    }

    /// <summary>
    /// Applies a function to each element in a list and then returns a list of values where the applied function
    /// returned Some(value). Returns an empty list when the input list is empty or when the applied chooser function
    /// returns None for all elements.
    /// </summary>
    /// <param name="source">The input <c>List&lt;TValue&gt;</c>.</param>
    /// <param name="chooser">The function to be applied to the list elements.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>
    /// The resulting <c>List&lt;TOutput&gt;</c> comprising the values where the chooser function returned Some(value).
    /// </returns>
    public static List<TOutput> Choose<TValue, TOutput>(this List<TValue> source, Func<TValue, Option<TOutput>> chooser)
        where TValue : notnull
        where TOutput : notnull
    {
        return source.AsEnumerable().Choose(chooser).ToList();
    }

    /// <summary>
    /// Applies a function to each element in an array and then returns an array of values where the applied function
    /// returned Some(value). Returns an empty array when the input array is empty or when the applied chooser function
    /// returns None for all elements.
    /// </summary>
    /// <param name="source">The input <c>TValue[]</c>.</param>
    /// <param name="chooser">The function to be applied to the array elements.</param>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TOutput">The type of the output value.</typeparam>
    /// <returns>
    /// The resulting <c>TOutput[]</c> comprising the values where the chooser function returned Some(value).
    /// </returns>
    public static TOutput[] Choose<TValue, TOutput>(this TValue[] source, Func<TValue, Option<TOutput>> chooser)
        where TValue : notnull
        where TOutput : notnull
    {
        return source.AsEnumerable().Choose(chooser).ToArray();
    }
}