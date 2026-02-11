namespace MudExtended.Models.Filtering;

/// <summary>
/// Filter definition for a field.
/// </summary>
public record FilterDefinition
{
    /// <summary>
    /// Field name.
    /// </summary>
    public required string FieldName { get; init; }

    /// <summary>
    /// Display label.
    /// </summary>
    public required string Label { get; init; }

    /// <summary>
    /// Field type.
    /// </summary>
    public required FilterFieldType FieldType { get; init; }

    /// <summary>
    /// Allowed operators.
    /// </summary>
    public List<FilterOperator> AllowedOperators { get; init; } = [];

    /// <summary>
    /// Available options (for Select/MultiSelect).
    /// </summary>
    public List<FilterOption> Options { get; init; } = [];

    /// <summary>
    /// Is field required for filtering?
    /// </summary>
    public bool IsRequired { get; init; }

    /// <summary>
    /// Default operator.
    /// </summary>
    public FilterOperator DefaultOperator { get; init; } = FilterOperator.Equals;

    /// <summary>
    /// Placeholder text.
    /// </summary>
    public string? Placeholder { get; init; }

    /// <summary>
    /// Help text.
    /// </summary>
    public string? HelpText { get; init; }
}

/// <summary>
/// Filter option for Select/MultiSelect fields.
/// </summary>
public record FilterOption
{
    /// <summary>
    /// Display text.
    /// </summary>
    public required string Label { get; init; }

    /// <summary>
    /// Option value.
    /// </summary>
    public required object Value { get; init; }
}
