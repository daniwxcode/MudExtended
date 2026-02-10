using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using MudExtended.Models.Permissions;

namespace MudExtended.Services.Permissions;

/// <summary>
/// Impl�mentation du service de v�rification des permissions.
/// </summary>
public class PermissionService : IPermissionService
{
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly IAuthorizationService _authorizationService;

    /// <summary>
    /// Initialise une nouvelle instance de <see cref="PermissionService"/>.
    /// </summary>
    public PermissionService(
        AuthenticationStateProvider authStateProvider,
        IAuthorizationService authorizationService)
    {
        _authStateProvider = authStateProvider;
        _authorizationService = authorizationService;
    }

    /// <inheritdoc/>
    public async Task<PermissionSet> GetPermissionsAsync(string resource)
    {
        var user = await GetUserAsync();
        if (user is null || !user.Identity?.IsAuthenticated == true)
            return PermissionSet.None;

        return new PermissionSet
        {
            CanCreate = await CheckPermissionAsync(user, "Create", resource),
            CanRead = await CheckPermissionAsync(user, "View", resource),
            CanUpdate = await CheckPermissionAsync(user, "Update", resource),
            CanDelete = await CheckPermissionAsync(user, "Delete", resource),
            CanSearch = await CheckPermissionAsync(user, "Search", resource),
            CanExport = await CheckPermissionAsync(user, "Export", resource),
            CanSubmit = await CheckPermissionAsync(user, "Submit", resource),
            CanPay = await CheckPermissionAsync(user, "Pay", resource)
        };
    }

    /// <inheritdoc/>
    public async Task<bool> HasPermissionAsync(string action, string resource)
    {
        var user = await GetUserAsync();
        if (user is null)
            return false;

        return await CheckPermissionAsync(user, action, resource);
    }

    /// <inheritdoc/>
    public async Task<bool> IsAuthenticatedAsync()
    {
        var user = await GetUserAsync();
        return user?.Identity?.IsAuthenticated == true;
    }

    private async Task<ClaimsPrincipal?> GetUserAsync()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        return authState.User;
    }

    private async Task<bool> CheckPermissionAsync(ClaimsPrincipal user, string action, string resource)
    {
        if (string.IsNullOrWhiteSpace(action) || string.IsNullOrWhiteSpace(resource))
            return false;

        // Format de policy attendu: "Permission.Resource.Action"
        var policyName = $"Permission.{resource}.{action}";
        
        try
        {
            var result = await _authorizationService.AuthorizeAsync(user, null, policyName);
            return result.Succeeded;
        }
        catch
        {
            // Policy non d�finie, v�rifie via claims
            var permissionClaim = $"Permissions.{resource}.{action}";
            return user.HasClaim(c => c.Type == "Permission" && c.Value == permissionClaim);
        }
    }
}
