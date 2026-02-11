using MudExtended.Models.Configuration;
using MudExtended.Models.Localization;
using System.Globalization;

namespace MudExtended.Services.Localization;

/// <summary>
/// Localization service implementation with multilingual support (EN, FR, ES).
/// </summary>
public sealed class LocalizationService : ILocalizationService
{
    private SupportedLanguage _currentLanguage;
    private readonly Dictionary<string, Dictionary<SupportedLanguage, string>> _resources = [];

    public SupportedLanguage CurrentLanguage => _currentLanguage;

    public event EventHandler<SupportedLanguage>? LanguageChanged;

    /// <summary>
    /// Initialize localization service with default language from options.
    /// </summary>
    public LocalizationService(MudExtendedOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        _currentLanguage = options.DefaultLanguage;
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
        AddResource("Common.Details", ("Détails", "Details", "Detalles"));
        AddResource("Common.Refresh", ("Actualiser", "Refresh", "Actualizar"));
        AddResource("Common.Actions", ("Actions", "Actions", "Acciones"));
        AddResource("Common.Confirm", ("Confirmer", "Confirm", "Confirmar"));
        AddResource("Common.Yes", ("Oui", "Yes", "Sí"));
        AddResource("Common.No", ("Non", "No", "No"));

        // Table
        AddResource("Table.AddButton", ("Ajouter", "Add", "Agregar"));
        AddResource("Table.RefreshButton", ("Actualiser", "Refresh", "Actualizar"));
        AddResource("Table.SearchPlaceholder", ("Rechercher...", "Search...", "Buscar..."));
        AddResource("Table.ActionsColumn", ("Actions", "Actions", "Acciones"));
        AddResource("Table.EditAction", ("Modifier", "Edit", "Editar"));
        AddResource("Table.DeleteAction", ("Supprimer", "Delete", "Eliminar"));
        AddResource("Table.DetailsAction", ("Détails", "Details", "Detalles"));
        AddResource("Table.NoRecords", ("Aucun enregistrement trouvé", "No records found", "No se encontraron registros"));
        AddResource("Table.Loading", ("Chargement...", "Loading...", "Cargando..."));
        AddResource("Table.RowsPerPage", ("Lignes par page :", "Rows per page:", "Filas por página:"));
        AddResource("Table.PaginationFormat", ("{first_item}-{last_item} sur {all_items}", "{first_item}-{last_item} of {all_items}", "{first_item}-{last_item} de {all_items}"));
        AddResource("Table.ConfirmDeleteTitle", ("Confirmer la suppression", "Confirm Deletion", "Confirmar eliminación"));
        AddResource("Table.ConfirmDeleteMessage", ("Êtes-vous sûr de vouloir supprimer cet élément ?", "Are you sure you want to delete this item?", "¿Está seguro de que desea eliminar este elemento?"));

        // Dialog
        AddResource("Dialog.SaveButton", ("Enregistrer", "Save", "Guardar"));
        AddResource("Dialog.CancelButton", ("Annuler", "Cancel", "Cancelar"));
        AddResource("Dialog.CloseButton", ("Fermer", "Close", "Cerrar"));
        AddResource("Dialog.ConfirmButton", ("Confirmer", "Confirm", "Confirmar"));

        // Empty State
        AddResource("EmptyState.DefaultTitle", ("Aucun résultat", "No results", "Sin resultados"));
        AddResource("EmptyState.DefaultDescription", ("Créez votre premier élément", "Create your first item", "Cree su primer artículo"));

        // Bulk Actions
        AddResource("BulkActions.Selected", ("{0} sélectionné(s)", "{0} selected", "{0} seleccionado(s)"));
        AddResource("BulkActions.AllSelected", ("Tous sélectionnés", "All selected", "Todos seleccionados"));
        AddResource("BulkActions.Clear", ("Effacer", "Clear", "Limpiar"));

        // DataExporter
        AddResource("DataExporter.Tooltip", ("Exporter les données", "Export data", "Exportar datos"));
        AddResource("DataExporter.NoData", ("Aucune donnée à exporter", "No data to export", "Sin datos para exportar"));
        AddResource("DataExporter.ExportAsCsv", ("Exporter en CSV", "Export as CSV", "Exportar como CSV"));
        AddResource("DataExporter.ExportAsJson", ("Exporter en JSON", "Export as JSON", "Exportar como JSON"));
        AddResource("DataExporter.ExportAsExcel", ("Exporter en Excel", "Export as Excel", "Exportar como Excel"));
        AddResource("DataExporter.ExportAsXml", ("Exporter en XML", "Export as XML", "Exportar como XML"));
        AddResource("DataExporter.ExportSettings", ("Paramètres d'exportation", "Export Settings", "Configuración de exportación"));
        AddResource("DataExporter.ExportSuccess", ("Exporté avec succès en {0}", "Exported successfully as {0}", "Exportado exitosamente como {0}"));
        AddResource("DataExporter.ExportError", ("Erreur d'exportation : {0}", "Export failed: {0}", "Error de exportación: {0}"));

        // FormWizard
        AddResource("FormWizard.Next", ("Suivant", "Next", "Siguiente"));
        AddResource("FormWizard.Previous", ("Précédent", "Previous", "Anterior"));
        AddResource("FormWizard.Complete", ("Terminer", "Complete", "Completar"));
        AddResource("FormWizard.StepValidationFailed", ("La validation de l'étape a échoué", "Step validation failed", "La validación del paso falló"));
        AddResource("FormWizard.FinalValidationFailed", ("La validation finale a échoué", "Final step validation failed", "La validación final falló"));

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
        AddResource("Filter.Field", ("Champ", "Field", "Campo"));
        AddResource("Filter.Operator", ("Opérateur", "Operator", "Operador"));
        AddResource("Filter.Value", ("Valeur", "Value", "Valor"));
        AddResource("Filter.From", ("De", "From", "Desde"));
        AddResource("Filter.To", ("À", "To", "Hasta"));
        AddResource("Filter.AddCriteria", ("Ajouter un critère", "Add criteria", "Agregar criterio"));
        AddResource("Filter.RemoveCriteria", ("Supprimer le critère", "Remove criteria", "Eliminar criterio"));
        AddResource("Filter.Apply", ("Appliquer", "Apply", "Aplicar"));
        AddResource("Filter.Reset", ("Réinitialiser", "Reset", "Restablecer"));
        AddResource("Filter.SaveAsTemplate", ("Enregistrer comme modèle", "Save as template", "Guardar como plantilla"));
        AddResource("Filter.Op.Equals", ("Égal à", "Equals", "Igual a"));
        AddResource("Filter.Op.NotEquals", ("Différent de", "Not equals", "Diferente de"));
        AddResource("Filter.Op.Contains", ("Contient", "Contains", "Contiene"));
        AddResource("Filter.Op.NotContains", ("Ne contient pas", "Not contains", "No contiene"));
        AddResource("Filter.Op.StartsWith", ("Commence par", "Starts with", "Comienza con"));
        AddResource("Filter.Op.EndsWith", ("Se termine par", "Ends with", "Termina con"));
        AddResource("Filter.Op.GreaterThan", ("Supérieur à", "Greater than", "Mayor que"));
        AddResource("Filter.Op.LessThan", ("Inférieur à", "Less than", "Menor que"));
        AddResource("Filter.Op.GreaterThanOrEquals", ("Supérieur ou égal à", "Greater than or equals", "Mayor o igual que"));
        AddResource("Filter.Op.LessThanOrEquals", ("Inférieur ou égal à", "Less than or equals", "Menor o igual que"));
        AddResource("Filter.Op.Between", ("Entre", "Between", "Entre"));
        AddResource("Filter.Op.In", ("Dans la liste", "In list", "En la lista"));
        AddResource("Filter.Op.IsEmpty", ("Est vide", "Is empty", "Está vacío"));
        AddResource("Filter.Op.IsNotEmpty", ("N'est pas vide", "Is not empty", "No está vacío"));

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
