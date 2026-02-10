namespace MudExtended.Models.Configuration;

/// <summary>
/// Textes localis�s pour les tables.
/// </summary>
public record TableLocalization
{
    /// <summary>Texte du bouton Ajouter.</summary>
    public string AddButton { get; init; } = "Ajouter";

    /// <summary>Texte du bouton Rafra�chir.</summary>
    public string RefreshButton { get; init; } = "Rafra�chir";

    /// <summary>Placeholder du champ de recherche.</summary>
    public string SearchPlaceholder { get; init; } = "Rechercher...";

    /// <summary>Titre de la colonne Actions.</summary>
    public string ActionsColumn { get; init; } = "Actions";

    /// <summary>Texte de l'action Modifier.</summary>
    public string EditAction { get; init; } = "Modifier";

    /// <summary>Texte de l'action Supprimer.</summary>
    public string DeleteAction { get; init; } = "Supprimer";

    /// <summary>Texte de l'action D�tails.</summary>
    public string DetailsAction { get; init; } = "D�tails";

    /// <summary>Message lorsque la liste est vide.</summary>
    public string NoRecordsMessage { get; init; } = "Aucun �l�ment trouv�";

    /// <summary>Message de chargement.</summary>
    public string LoadingMessage { get; init; } = "Chargement...";

    /// <summary>Libell� pour les lignes par page.</summary>
    public string RowsPerPage { get; init; } = "Lignes par page:";

    /// <summary>Format de pagination (utilise {first_item}, {last_item}, {all_items}).</summary>
    public string PaginationFormat { get; init; } = "{first_item}-{last_item} sur {all_items}";

    /// <summary>Titre du dialog de confirmation de suppression.</summary>
    public string ConfirmDeleteTitle { get; init; } = "Confirmer la suppression";

    /// <summary>Message du dialog de confirmation de suppression.</summary>
    public string ConfirmDeleteMessage { get; init; } = "Voulez-vous vraiment supprimer cet �l�ment ?";
}
