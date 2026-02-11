namespace MudExtended.Models.Export;

/// <summary>
/// Export options and configuration.
/// </summary>
public record ExportOptions
{
    /// <summary>
    /// Include headers in export.
    /// </summary>
    public bool IncludeHeaders { get; init; } = true;

    /// <summary>
    /// File name (without extension).
    /// </summary>
    public string FileName { get; init; } = "export";

    /// <summary>
    /// Include timestamps in file name.
    /// </summary>
    public bool IncludeTimestamp { get; init; } = true;

    /// <summary>
    /// Target sheet name (Excel only).
    /// </summary>
    public string? SheetName { get; init; } = "Data";

    /// <summary>
    /// Include row numbers.
    /// </summary>
    public bool IncludeRowNumbers { get; init; }

    /// <summary>
    /// Columns to export.
    /// </summary>
    public List<ExportColumn> Columns { get; init; } = [];

    /// <summary>
    /// Custom header mapping.
    /// </summary>
    public Dictionary<string, string> HeaderMappings { get; init; } = [];

    /// <summary>
    /// Max rows to export (0 = no limit).
    /// </summary>
    public int MaxRows { get; init; }
}
