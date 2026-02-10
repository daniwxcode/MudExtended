using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudExtended.Models.Permissions;
using MudExtended.Services.Permissions;

namespace MudExtended.Components.Base;

/// <summary>
/// Classe de base abstraite pour les composants n�cessitant des v�rifications d'autorisation.
/// </summary>
public abstract class AuthorizedComponentBase : ComponentBase
{
    /// <summary>
    /// �tat d'authentification en cascade.
    /// </summary>
    [CascadingParameter]
    protected Task<AuthenticationState>? AuthState { get; set; }

    /// <summary>
    /// Service de permissions inject�.
    /// </summary>
    [Inject]
    protected IPermissionService PermissionService { get; set; } = default!;

    /// <summary>
    /// Ressource � v�rifier (ex: "Cars", "Countries").
    /// </summary>
    protected abstract string Resource { get; }

    /// <summary>
    /// Ensemble des permissions calcul�es.
    /// </summary>
    protected PermissionSet Permissions { get; private set; } = new();

    /// <summary>
    /// Indique si les permissions sont charg�es.
    /// </summary>
    protected bool IsPermissionsLoaded { get; private set; }

    /// <summary>
    /// Indique si le composant est en cours de chargement initial.
    /// </summary>
    protected bool IsInitializing { get; private set; } = true;

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        await LoadPermissionsAsync();
        await base.OnInitializedAsync();
        IsInitializing = false;
    }

    /// <summary>
    /// Charge les permissions pour la ressource.
    /// </summary>
    protected virtual async Task LoadPermissionsAsync()
    {
        if (string.IsNullOrWhiteSpace(Resource))
        {
            Permissions = PermissionSet.None;
            IsPermissionsLoaded = true;
            return;
        }

        try
        {
            Permissions = await PermissionService.GetPermissionsAsync(Resource);
        }
        catch
        {
            Permissions = PermissionSet.None;
        }
        finally
        {
            IsPermissionsLoaded = true;
        }
    }

    /// <summary>
    /// Recharge les permissions.
    /// </summary>
    protected async Task RefreshPermissionsAsync()
    {
        IsPermissionsLoaded = false;
        await LoadPermissionsAsync();
        StateHasChanged();
    }

    /// <summary>
    /// V�rifie si une action sp�cifique est autoris�e.
    /// </summary>
    /// <param name="action">Action � v�rifier.</param>
    /// <returns>True si autoris�.</returns>
    protected async Task<bool> HasPermissionAsync(string action)
    {
        return await PermissionService.HasPermissionAsync(action, Resource);
    }
}
