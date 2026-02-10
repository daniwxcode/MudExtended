namespace MudExtended.Models.Pagination;

/// <summary>
/// Filtre de pagination pour les requ�tes API.
/// </summary>
public record PaginationFilter
{
    /// <summary>Num�ro de page (1-based).</summary>
    public int PageNumber { get; init; } = 1;

    /// <summary>Taille de page.</summary>
    public int PageSize { get; init; } = 10;

    /// <summary>Mot-cl� de recherche.</summary>
    public string? Keyword { get; init; }

    /// <summary>Champs de tri (ex: "Name asc, Date desc").</summary>
    public string[]? OrderBy { get; init; }

    /// <summary>Cr�e un nouveau filtre avec le mot-cl� modifi�.</summary>
    public PaginationFilter WithKeyword(string? keyword) => 
        this with { Keyword = keyword, PageNumber = 1 };

    /// <summary>Cr�e un nouveau filtre avec la page modifi�e.</summary>
    public PaginationFilter WithPage(int page) => 
        this with { PageNumber = page };

    /// <summary>Cr�e un nouveau filtre avec la taille modifi�e.</summary>
    public PaginationFilter WithPageSize(int size) => 
        this with { PageSize = size, PageNumber = 1 };

    /// <summary>Cr�e un nouveau filtre avec le tri modifi�.</summary>
    public PaginationFilter WithOrderBy(params string[] orderBy) => 
        this with { OrderBy = orderBy };
}

/// <summary>
/// R�sultat pagin� d'une requ�te API.
/// </summary>
/// <typeparam name="T">Type des �l�ments.</typeparam>
public record PagedResult<T>
{
    /// <summary>Liste des �l�ments.</summary>
    public IReadOnlyList<T> Data { get; init; } = [];

    /// <summary>Page actuelle.</summary>
    public int CurrentPage { get; init; } = 1;

    /// <summary>Taille de page.</summary>
    public int PageSize { get; init; } = 10;

    /// <summary>Nombre total d'�l�ments.</summary>
    public int TotalCount { get; init; }

    /// <summary>Nombre total de pages.</summary>
    public int TotalPages { get; init; }

    /// <summary>Indique si une page pr�c�dente existe.</summary>
    public bool HasPreviousPage => CurrentPage > 1;

    /// <summary>Indique si une page suivante existe.</summary>
    public bool HasNextPage => CurrentPage < TotalPages;

    /// <summary>R�sultat vide.</summary>
    public static PagedResult<T> Empty => new();

    /// <summary>Convertit en PaginationState.</summary>
    public PaginationState ToPaginationState() => new()
    {
        CurrentPage = CurrentPage,
        PageSize = PageSize,
        TotalItems = TotalCount
    };
}
