namespace Functional;

/// <summary>
/// The <c>()</c> type, also called 'unit'. The <c>()</c> type has exactly one value <c>()</c>,
/// and is used when there is no other meaningful value that could be returned.
/// </summary>
public readonly record struct Unit
{
    /// <summary>
    /// Returns the default value of <c>()</c>.
    /// </summary>
    public static readonly Unit Default = default;

    public override string ToString() => "()";
}