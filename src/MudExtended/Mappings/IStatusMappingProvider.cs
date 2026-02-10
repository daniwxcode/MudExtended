using MudBlazor;

namespace MudExtended.Mappings;

/// <summary>
/// Provider de mapping pour un type de statut.
/// </summary>
/// <typeparam name="TStatus">Type d'�num�ration du statut.</typeparam>
public interface IStatusMappingProvider<in TStatus> where TStatus : Enum
{
    /// <summary>Obtient la couleur pour le statut.</summary>
    /// <param name="status">Valeur du statut.</param>
    /// <returns>Couleur MudBlazor.</returns>
    Color GetColor(TStatus status);

    /// <summary>Obtient l'ic�ne pour le statut.</summary>
    /// <param name="status">Valeur du statut.</param>
    /// <returns>Ic�ne Material.</returns>
    string GetIcon(TStatus status);

    /// <summary>Obtient le libell� pour le statut.</summary>
    /// <param name="status">Valeur du statut.</param>
    /// <returns>Libell� localis�.</returns>
    string GetLabel(TStatus status);

    /// <summary>Obtient la classe CSS pour le statut (optionnel).</summary>
    /// <param name="status">Valeur du statut.</param>
    /// <returns>Classe CSS ou null.</returns>
    string? GetCssClass(TStatus status) => null;
}
