using MudExtended.Models.Permissions;

namespace MudExtended.Services.Permissions;

/// <summary>
/// Service de v�rification des permissions.
/// </summary>
public interface IPermissionService
{
    /// <summary>
    /// Obtient l'ensemble des permissions pour une ressource.
    /// </summary>
    /// <param name="resource">Nom de la ressource.</param>
    /// <returns>Ensemble des permissions.</returns>
    Task<PermissionSet> GetPermissionsAsync(string resource);

    /// <summary>
    /// V�rifie si une action est autoris�e sur une ressource.
    /// </summary>
    /// <param name="action">Action � v�rifier.</param>
    /// <param name="resource">Ressource cible.</param>
    /// <returns>True si autoris�.</returns>
    Task<bool> HasPermissionAsync(string action, string resource);

    /// <summary>
    /// V�rifie si l'utilisateur est authentifi�.
    /// </summary>
    /// <returns>True si authentifi�.</returns>
    Task<bool> IsAuthenticatedAsync();
}
