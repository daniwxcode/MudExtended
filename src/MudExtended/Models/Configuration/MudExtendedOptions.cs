using System.Globalization;

namespace MudExtended.Models.Configuration;

/// <summary>
/// Options de configuration de MudExtended.
/// </summary>
public class MudExtendedOptions
{
    /// <summary>Localisation par d�faut pour les tables.</summary>
    public TableLocalization DefaultTableLocalization { get; set; } = new();

    /// <summary>Activer le loader global.</summary>
    public bool UseGlobalLoader { get; set; } = true;

    /// <summary>Format de date par d�faut.</summary>
    public string DateFormat { get; set; } = "dd/MM/yyyy";

    /// <summary>Format de date et heure par d�faut.</summary>
    public string DateTimeFormat { get; set; } = "dd/MM/yyyy HH:mm";

    /// <summary>Culture par d�faut.</summary>
    public CultureInfo Culture { get; set; } = CultureInfo.GetCultureInfo("fr-FR");

    /// <summary>Enregistrer les mappings de statut par d�faut.</summary>
    public bool RegisterDefaultMappings { get; set; } = true;

    /// <summary>Configuration par d�faut des dialogs.</summary>
    public DialogConfiguration DefaultDialogConfiguration { get; set; } = new();

    /// <summary>Symbole mon�taire par d�faut.</summary>
    public string CurrencySymbol { get; set; } = "FCFA";

    /// <summary>D�lai de debounce pour la recherche (ms).</summary>
    public int SearchDebounceDelay { get; set; } = 300;

    /// <summary>Tailles de page disponibles.</summary>
    public int[] PageSizeOptions { get; set; } = [10, 25, 50, 100];

    /// <summary>Taille de page par défaut.</summary>
    public int DefaultPageSize { get; set; } = 10;

    /// <summary>
    /// Active le service de permissions. Nécessite que AuthenticationStateProvider 
    /// et IAuthorizationService soient enregistrés dans le conteneur de dépendances.
    /// </summary>
    public bool UsePermissionService { get; set; } = false;
}
