using MudBlazor;

namespace MudExtended.Models.Configuration;

/// <summary>
/// Configuration par d�faut des dialogs.
/// </summary>
public record DialogConfiguration
{
    /// <summary>Largeur maximale du dialog.</summary>
    public MaxWidth MaxWidth { get; init; } = MaxWidth.Medium;

    /// <summary>Utiliser la pleine largeur.</summary>
    public bool FullWidth { get; init; } = true;

    /// <summary>Afficher le bouton de fermeture.</summary>
    public bool CloseButton { get; init; } = true;

    /// <summary>Fermer avec la touche �chap.</summary>
    public bool CloseOnEscapeKey { get; init; } = true;

    /// <summary>Fermer au clic sur le backdrop.</summary>
    public bool BackdropClick { get; init; } = false;

    /// <summary>Masquer l'en-t�te.</summary>
    public bool NoHeader { get; init; } = false;

    /// <summary>Convertit en DialogOptions MudBlazor.</summary>
    public DialogOptions ToDialogOptions() => new()
    {
        MaxWidth = MaxWidth,
        FullWidth = FullWidth,
        CloseButton = CloseButton,
        CloseOnEscapeKey = CloseOnEscapeKey,
        BackdropClick = BackdropClick,
        NoHeader = NoHeader
    };
}
