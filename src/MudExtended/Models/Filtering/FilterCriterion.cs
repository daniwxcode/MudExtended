namespace MudExtended.Models.Filtering;

/// <summary>
/// Single filter criterion.
/// </summary>
public record FilterCriterion
{
    /// <summary>
    /// Field name to filter on.
    /// </summary>
    public required string FieldName { get; init; }

    /// <summary>
    /// Filter operator.
    /// </summary>
    public required FilterOperator Operator { get; init; }

    /// <summary>
    /// Filter value(s).
    /// </summary>
    public object? Value { get; init; }

    /// <summary>
    /// Secondary value (for Between operator).
    /// </summary>
    public object? ValueTo { get; init; }

    /// <summary>
    /// Is this criterion active?
    /// </summary>
    public bool IsActive { get; init; } = true;
}
