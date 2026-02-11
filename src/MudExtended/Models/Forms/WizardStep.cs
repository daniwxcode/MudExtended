using Microsoft.AspNetCore.Components;

namespace MudExtended.Models.Forms;

/// <summary>
/// Wizard step definition.
/// </summary>
public record WizardStep
{
    /// <summary>
    /// Unique step identifier.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Display title.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// Optional description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Step icon.
    /// </summary>
    public string? Icon { get; init; }

    /// <summary>
    /// Step order/sequence.
    /// </summary>
    public int Order { get; init; }

    /// <summary>
    /// Is step optional?
    /// </summary>
    public bool IsOptional { get; init; }

    /// <summary>
    /// Can skip this step?
    /// </summary>
    public bool CanSkip { get; init; }

    /// <summary>
    /// Is step completed?
    /// </summary>
    public bool IsCompleted { get; init; }

    /// <summary>
    /// Is step in error state?
    /// </summary>
    public bool HasError { get; init; }

    /// <summary>
    /// Error message.
    /// </summary>
    public string? ErrorMessage { get; init; }

    /// <summary>
    /// Step content/template.
    /// </summary>
    public RenderFragment? Content { get; init; }
}
