namespace MudExtended.Models.Configuration;

/// <summary>
/// Localized texts for tables.
/// </summary>
public record TableLocalization
{
    /// <summary>Add button text.</summary>
    public string AddButton { get; init; } = "Add";

    /// <summary>Refresh button text.</summary>
    public string RefreshButton { get; init; } = "Refresh";

    /// <summary>Search field placeholder.</summary>
    public string SearchPlaceholder { get; init; } = "Search...";

    /// <summary>Actions column title.</summary>
    public string ActionsColumn { get; init; } = "Actions";

    /// <summary>Edit action text.</summary>
    public string EditAction { get; init; } = "Edit";

    /// <summary>Delete action text.</summary>
    public string DeleteAction { get; init; } = "Delete";

    /// <summary>Details action text.</summary>
    public string DetailsAction { get; init; } = "Details";

    /// <summary>Message when list is empty.</summary>
    public string NoRecordsMessage { get; init; } = "No records found";

    /// <summary>Loading message.</summary>
    public string LoadingMessage { get; init; } = "Loading...";

    /// <summary>Rows per page label.</summary>
    public string RowsPerPage { get; init; } = "Rows per page:";

    /// <summary>Pagination format (uses {first_item}, {last_item}, {all_items}).</summary>
    public string PaginationFormat { get; init; } = "{first_item}-{last_item} of {all_items}";

    /// <summary>Delete confirmation dialog title.</summary>
    public string ConfirmDeleteTitle { get; init; } = "Confirm Deletion";

    /// <summary>Delete confirmation dialog message.</summary>
    public string ConfirmDeleteMessage { get; init; } = "Are you sure you want to delete this item?";
}
