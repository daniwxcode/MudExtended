namespace MudExtended.Models.Audit;

/// <summary>
/// Audit log entry.
/// </summary>
public record AuditEntry
{
    /// <summary>
    /// Unique entry identifier.
    /// </summary>
    public required string Id { get; init; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Entity type.
    /// </summary>
    public required string EntityType { get; init; }

    /// <summary>
    /// Entity identifier.
    /// </summary>
    public required object EntityId { get; init; }

    /// <summary>
    /// Action performed (Create, Update, Delete, etc.).
    /// </summary>
    public required string Action { get; init; }

    /// <summary>
    /// User who performed the action.
    /// </summary>
    public required string UserId { get; init; }

    /// <summary>
    /// User name.
    /// </summary>
    public string? UserName { get; init; }

    /// <summary>
    /// Timestamp of action.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Previous state (before update).
    /// </summary>
    public Dictionary<string, object?>? OldValues { get; init; }

    /// <summary>
    /// New state (after update).
    /// </summary>
    public Dictionary<string, object?>? NewValues { get; init; }

    /// <summary>
    /// Changed properties.
    /// </summary>
    public List<string> ChangedProperties { get; init; } = [];

    /// <summary>
    /// Additional metadata.
    /// </summary>
    public Dictionary<string, string>? Metadata { get; init; }

    /// <summary>
    /// IP address of requester.
    /// </summary>
    public string? IpAddress { get; init; }

    /// <summary>
    /// User agent.
    /// </summary>
    public string? UserAgent { get; init; }
}
