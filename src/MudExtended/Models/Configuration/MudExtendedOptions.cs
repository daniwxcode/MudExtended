using System.Globalization;
using MudExtended.Models.Localization;

namespace MudExtended.Models.Configuration;

/// <summary>
/// Options de configuration de MudExtended.
/// </summary>
public class MudExtendedOptions
{
    /// <summary>Localisation par défaut pour les tables.</summary>
    public TableLocalization DefaultTableLocalization { get; set; } = new();

    /// <summary>Activer le loader global.</summary>
    public bool UseGlobalLoader { get; set; } = true;

    /// <summary>Format de date par défaut.</summary>
    public string DateFormat { get; set; } = "dd/MM/yyyy";

    /// <summary>Format de date et heure par défaut.</summary>
    public string DateTimeFormat { get; set; } = "dd/MM/yyyy HH:mm";

    /// <summary>Culture par défaut.</summary>
    public CultureInfo Culture { get; set; } = CultureInfo.GetCultureInfo("fr-FR");

    /// <summary>Enregistrer les mappings de statut par défaut.</summary>
    public bool RegisterDefaultMappings { get; set; } = true;

    /// <summary>Configuration par défaut des dialogs.</summary>
    public DialogConfiguration DefaultDialogConfiguration { get; set; } = new();

    /// <summary>Symbole monétaire par défaut.</summary>
    public string CurrencySymbol { get; set; } = "FCFA";

    /// <summary>Délai de debounce pour la recherche (ms).</summary>
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

    /// <summary>
    /// Langue par défaut pour la localisation.
    /// </summary>
    public SupportedLanguage DefaultLanguage { get; set; } = SupportedLanguage.English;

    /// <summary>
    /// Activer la journalisation d'audit.
    /// </summary>
    public bool EnableAuditLogging { get; set; } = true;

    /// <summary>
    /// Rétention des logs d'audit en jours.
    /// </summary>
    public int AuditLogRetentionDays { get; set; } = 90;

    /// <summary>
    /// Nombre maximum de notifications.
    /// </summary>
    public int MaxNotifications { get; set; } = 10;
}
