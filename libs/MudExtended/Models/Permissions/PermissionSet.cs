namespace MudExtended.Models.Permissions;

/// <summary>
/// Ensemble des permissions CRUD pour une ressource.
/// </summary>
public record PermissionSet
{
    /// <summary>Permission de cr�ation.</summary>
    public bool CanCreate { get; init; }

    /// <summary>Permission de lecture.</summary>
    public bool CanRead { get; init; }

    /// <summary>Permission de mise � jour.</summary>
    public bool CanUpdate { get; init; }

    /// <summary>Permission de suppression.</summary>
    public bool CanDelete { get; init; }

    /// <summary>Permission de recherche.</summary>
    public bool CanSearch { get; init; }

    /// <summary>Permission d'export.</summary>
    public bool CanExport { get; init; }

    /// <summary>Permission de soumission.</summary>
    public bool CanSubmit { get; init; }

    /// <summary>Permission de paiement.</summary>
    public bool CanPay { get; init; }

    /// <summary>Indique si au moins une action CRUD est autoris�e.</summary>
    public bool HasAnyAction => CanCreate || CanUpdate || CanDelete;

    /// <summary>Indique si toutes les actions CRUD sont autoris�es.</summary>
    public bool HasAllCrud => CanCreate && CanRead && CanUpdate && CanDelete;

    /// <summary>Ensemble vide (aucune permission).</summary>
    public static PermissionSet None => new();

    /// <summary>Ensemble complet (toutes les permissions).</summary>
    public static PermissionSet All => new()
    {
        CanCreate = true,
        CanRead = true,
        CanUpdate = true,
        CanDelete = true,
        CanSearch = true,
        CanExport = true,
        CanSubmit = true,
        CanPay = true
    };
}
