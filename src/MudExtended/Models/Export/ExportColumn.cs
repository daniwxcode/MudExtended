namespace MudExtended.Models.Export;

/// <summary>
/// Export column definition.
/// </summary>
public record ExportColumn
{
    /// <summary>
    /// Column header.
    /// </summary>
    public required string Header { get; init; }

    /// <summary>
    /// Property name for value extraction.
    /// </summary>
    public required string PropertyName { get; init; }

    /// <summary>
    /// Format the value (optional).
    /// </summary>
    public Func<object?, string>? Formatter { get; init; }

    /// <summary>
    /// Is column included in export?
    /// </summary>
    public bool IsIncluded { get; init; } = true;

    /// <summary>
    /// Column width (for Excel).
    /// </summary>
    public int? Width { get; init; }
}
