using MudExtended.Models.Audit;

namespace MudExtended.Services.Audit;

/// <summary>
/// Service for audit logging and retrieval.
/// </summary>
public interface IAuditService
{
    /// <summary>
    /// Log an audit entry.
    /// </summary>
    /// <param name="entry">Audit entry to log</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task LogAsync(AuditEntry entry, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get audit trail for an entity.
    /// </summary>
    /// <param name="entityType">Entity type</param>
    /// <param name="entityId">Entity identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of audit entries</returns>
    Task<List<AuditEntry>> GetAuditTrailAsync(
        string entityType,
        object entityId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get audit entries with filtering.
    /// </summary>
    /// <param name="entityType">Entity type filter</param>
    /// <param name="userId">User ID filter</param>
    /// <param name="startDate">Start date filter</param>
    /// <param name="endDate">End date filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<List<AuditEntry>> SearchAuditLogsAsync(
        string? entityType = null,
        string? userId = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get changes between two audit entries.
    /// </summary>
    /// <param name="oldEntry">Original entry</param>
    /// <param name="newEntry">Updated entry</param>
    /// <returns>Dictionary of changes</returns>
    Dictionary<string, (object? oldValue, object? newValue)> GetChanges(AuditEntry oldEntry, AuditEntry newEntry);
}
