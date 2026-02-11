using MudBlazor;

namespace MudExtended.Components.Bulk;

/// <summary>
/// Bulk action definition.
/// </summary>
public record BulkAction
{
    /// <summary>
    /// Unique action identifier.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Display label.
    /// </summary>
    public required string Label { get; init; }

    /// <summary>
    /// Icon name.
    /// </summary>
    public string? Icon { get; init; }

    /// <summary>
    /// Button color.
    /// </summary>
    public Color Color { get; init; } = Color.Primary;

    /// <summary>
    /// Requires confirmation?
    /// </summary>
    public bool RequiresConfirmation { get; init; }

    /// <summary>
    /// Confirmation message.
    /// </summary>
    public string? ConfirmationMessage { get; init; }

    /// <summary>
    /// Is action disabled?
    /// </summary>
    public bool IsDisabled { get; init; }
}
