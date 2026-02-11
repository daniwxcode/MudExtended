namespace MudExtended.Models.Filtering;

/// <summary>
/// Complete filter configuration and state.
/// </summary>
public record FilterScheme
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Display name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Filter definitions for all available fields.
    /// </summary>
    public List<FilterDefinition> Definitions { get; init; } = [];

    /// <summary>
    /// Current active criteria.
    /// </summary>
    public List<FilterCriterion> Criteria { get; init; } = [];

    /// <summary>
    /// Logical operator between criteria (AND/OR).
    /// </summary>
    public FilterLogic Logic { get; init; } = FilterLogic.And;

    /// <summary>
    /// Is this a saved/template filter?
    /// </summary>
    public bool IsSaved { get; init; }

    /// <summary>
    /// Creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}

/// <summary>
/// Logical operator for combining criteria.
/// </summary>
public enum FilterLogic
{
    /// <summary>All criteria must match</summary>
    And,
    
    /// <summary>Any criterion can match</summary>
    Or
}
