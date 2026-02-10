namespace MudExtended.Models.Pagination;

/// <summary>
/// �tat de pagination pour les tables et listes.
/// </summary>
public record PaginationState
{
    /// <summary>Page actuelle (1-based).</summary>
    public int CurrentPage { get; init; } = 1;

    /// <summary>Nombre d'�l�ments par page.</summary>
    public int PageSize { get; init; } = 10;

    /// <summary>Nombre total d'�l�ments.</summary>
    public int TotalItems { get; init; }

    /// <summary>Nombre total de pages (calcul�).</summary>
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling(TotalItems / (double)PageSize) : 0;

    /// <summary>Index du premier �l�ment affich� (calcul�).</summary>
    public int DisplayFrom => TotalItems == 0 ? 0 : (CurrentPage - 1) * PageSize + 1;

    /// <summary>Index du dernier �l�ment affich� (calcul�).</summary>
    public int DisplayTo => Math.Min(CurrentPage * PageSize, TotalItems);

    /// <summary>Indique si une page pr�c�dente existe.</summary>
    public bool HasPreviousPage => CurrentPage > 1;

    /// <summary>Indique si une page suivante existe.</summary>
    public bool HasNextPage => CurrentPage < TotalPages;

    /// <summary>Cr�e un nouvel �tat avec la page modifi�e.</summary>
    /// <param name="page">Nouveau num�ro de page.</param>
    public PaginationState WithPage(int page) => this with { CurrentPage = page };

    /// <summary>Cr�e un nouvel �tat avec la taille modifi�e (reset � page 1).</summary>
    /// <param name="size">Nouvelle taille de page.</param>
    public PaginationState WithPageSize(int size) => this with { PageSize = size, CurrentPage = 1 };

    /// <summary>Cr�e un nouvel �tat avec le total d'�l�ments modifi�.</summary>
    /// <param name="total">Nouveau total d'�l�ments.</param>
    public PaginationState WithTotalItems(int total) => this with { TotalItems = total };
}
