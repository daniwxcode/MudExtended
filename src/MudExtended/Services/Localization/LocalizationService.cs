using MudExtended.Models.Localization;
using System.Globalization;

namespace MudExtended.Services.Localization;

/// <summary>
/// Localization service implementation.
/// </summary>
public sealed class LocalizationService : ILocalizationService
{
    private SupportedLanguage _currentLanguage = SupportedLanguage.English;
    private readonly Dictionary<string, Dictionary<SupportedLanguage, string>> _resources = [];

    public SupportedLanguage CurrentLanguage => _currentLanguage;

    public event EventHandler<SupportedLanguage>? LanguageChanged;

    /// <summary>
    /// Initialize localization service with resources.
    /// </summary>
    public LocalizationService()
    {
        LoadDefaultResources();
    }

    public string GetString(string key)
    {
        if (!_resources.TryGetValue(key, out var translations))
            return $"[{key}]";

        if (translations.TryGetValue(_currentLanguage, out var value))
            return value;

        // Fallback to English
        return translations.TryGetValue(SupportedLanguage.English, out var fallback)
            ? fallback
            : $"[{key}]";
    }

    public string GetString(string key, params object[] args)
    {
        try
        {
            var value = GetString(key);
            return string.Format(value, args);
        }
        catch
        {
            return GetString(key);
        }
    }

    public Task SetLanguageAsync(SupportedLanguage language)
    {
        if (_currentLanguage != language)
        {
            _currentLanguage = language;
            CultureInfo.CurrentUICulture = language switch
            {
                SupportedLanguage.French => new CultureInfo("fr-FR"),
                SupportedLanguage.Spanish => new CultureInfo("es-ES"),
                _ => new CultureInfo("en-US")
            };
            LanguageChanged?.Invoke(this, language);
        }

        return Task.CompletedTask;
    }

    private void LoadDefaultResources()
    {
        // Common
        AddResource("Common.Save", ("Enregistrer", "Save", "Guardar"));
        AddResource("Common.Cancel", ("Annuler", "Cancel", "Cancelar"));
        AddResource("Common.Delete", ("Supprimer", "Delete", "Eliminar"));
        AddResource("Common.Edit", ("Modifier", "Edit", "Editar"));
        AddResource("Common.Add", ("Ajouter", "Add", "Agregar"));
        AddResource("Common.Search", ("Rechercher", "Search", "Buscar"));
        AddResource("Common.Export", ("Exporter", "Export", "Exportar"));
        AddResource("Common.Import", ("Importer", "Import", "Importar"));
        AddResource("Common.Filter", ("Filtrer", "Filter", "Filtrar"));
        AddResource("Common.Settings", ("Paramètres", "Settings", "Configuración"));
        AddResource("Common.Loading", ("Chargement...", "Loading...", "Cargando..."));
        AddResource("Common.Error", ("Erreur", "Error", "Error"));
        AddResource("Common.Success", ("Succès", "Success", "Éxito"));
        AddResource("Common.Warning", ("Avertissement", "Warning", "Advertencia"));
        AddResource("Common.Info", ("Information", "Information", "Información"));
        AddResource("Common.NoData", ("Aucune donnée", "No data", "Sin datos"));

        // Empty State
        AddResource("EmptyState.DefaultTitle", ("Aucun résultat", "No results", "Sin resultados"));
        AddResource("EmptyState.DefaultDescription", ("Créez votre premier élément", "Create your first item", "Cree su primer artículo"));

        // Bulk Actions
        AddResource("BulkActions.Selected", ("sélectionné(s)", "selected", "seleccionado(s)"));
        AddResource("BulkActions.SelectAll", ("Tout sélectionner", "Select all", "Seleccionar todo"));
        AddResource("BulkActions.Clear", ("Effacer", "Clear", "Limpiar"));

        // DataExporter
        AddResource("DataExporter.ExportAs", ("Exporter en tant que {0}", "Export as {0}", "Exportar como {0}"));
        AddResource("DataExporter.ExportSuccess", ("Exporté avec succès en tant que {0}", "Exported successfully as {0}", "Exportado exitosamente como {0}"));
        AddResource("DataExporter.ExportError", ("Erreur d'exportation: {0}", "Export failed: {0}", "Error de exportación: {0}"));
        AddResource("DataExporter.NoDataToExport", ("Aucune donnée à exporter", "No data to export", "Sin datos para exportar"));

        // FormWizard
        AddResource("FormWizard.Next", ("Suivant", "Next", "Siguiente"));
        AddResource("FormWizard.Previous", ("Précédent", "Previous", "Anterior"));
        AddResource("FormWizard.Complete", ("Terminer", "Complete", "Completar"));
        AddResource("FormWizard.ValidationError", ("Validation échouée", "Validation failed", "Validación fallida"));

        // AuditLog
        AddResource("AuditLog.Action", ("Action", "Action", "Acción"));
        AddResource("AuditLog.User", ("Utilisateur", "User", "Usuario"));
        AddResource("AuditLog.Timestamp", ("Date/Heure", "Timestamp", "Marca de tiempo"));
        AddResource("AuditLog.Changes", ("Changements", "Changes", "Cambios"));
        AddResource("AuditLog.OldValue", ("Ancienne valeur", "Old value", "Valor anterior"));
        AddResource("AuditLog.NewValue", ("Nouvelle valeur", "New value", "Nuevo valor"));

        // Notifications
        AddResource("Notifications.Title", ("Notifications", "Notifications", "Notificaciones"));
        AddResource("Notifications.NoNotifications", ("Aucune notification", "No notifications", "Sin notificaciones"));
        AddResource("Notifications.ClearAll", ("Tout effacer", "Clear all", "Limpiar todo"));

        // AdvancedFilter
        AddResource("Filter.AddCriteria", ("Ajouter un critère", "Add criteria", "Agregar criterio"));
        AddResource("Filter.RemoveCriteria", ("Supprimer le critère", "Remove criteria", "Eliminar criterio"));
        AddResource("Filter.Apply", ("Appliquer", "Apply", "Aplicar"));
        AddResource("Filter.Reset", ("Réinitialiser", "Reset", "Restablecer"));
        AddResource("Filter.SaveAsTemplate", ("Enregistrer comme modèle", "Save as template", "Guardar como plantilla"));

        // Pagination
        AddResource("Pagination.PageOf", ("Page {0} sur {1}", "Page {0} of {1}", "Página {0} de {1}"));
        AddResource("Pagination.ItemsPerPage", ("Éléments par page", "Items per page", "Elementos por página"));
        AddResource("Pagination.Total", ("Total", "Total", "Total"));
    }

    private void AddResource(string key, (string French, string English, string Spanish) values)
    {
        _resources[key] = new Dictionary<SupportedLanguage, string>
        {
            { SupportedLanguage.French, values.French },
            { SupportedLanguage.English, values.English },
            { SupportedLanguage.Spanish, values.Spanish }
        };
    }
}
