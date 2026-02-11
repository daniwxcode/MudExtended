using MudExtended.Models.Audit;

namespace MudExtended.Services.Audit;

/// <summary>
/// In-memory audit service for testing/demo purposes.
/// </summary>
public sealed class InMemoryAuditService : IAuditService
{
    private static readonly List<AuditEntry> _auditLog = [];
    private readonly object _lockObject = new();

    public Task LogAsync(AuditEntry entry, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entry);

        lock (_lockObject)
        {
            _auditLog.Add(entry);
        }

        return Task.CompletedTask;
    }

    public Task<List<AuditEntry>> GetAuditTrailAsync(
        string entityType,
        object entityId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(entityType);
        ArgumentNullException.ThrowIfNull(entityId);

        lock (_lockObject)
        {
            var entries = _auditLog
                .Where(e => e.EntityType == entityType && e.EntityId.Equals(entityId))
                .OrderByDescending(e => e.Timestamp)
                .ToList();

            return Task.FromResult(entries);
        }
    }

    public Task<List<AuditEntry>> SearchAuditLogsAsync(
        string? entityType = null,
        string? userId = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        CancellationToken cancellationToken = default)
    {
        lock (_lockObject)
        {
            var query = _auditLog.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(entityType))
                query = query.Where(e => e.EntityType == entityType);

            if (!string.IsNullOrWhiteSpace(userId))
                query = query.Where(e => e.UserId == userId);

            if (startDate.HasValue)
                query = query.Where(e => e.Timestamp >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(e => e.Timestamp <= endDate.Value);

            var results = query.OrderByDescending(e => e.Timestamp).ToList();
            return Task.FromResult(results);
        }
    }

    public Dictionary<string, (object? oldValue, object? newValue)> GetChanges(AuditEntry oldEntry, AuditEntry newEntry)
    {
        ArgumentNullException.ThrowIfNull(oldEntry);
        ArgumentNullException.ThrowIfNull(newEntry);

        var changes = new Dictionary<string, (object?, object?)>();

        var oldValues = oldEntry.OldValues ?? new();
        var newValues = newEntry.NewValues ?? new();

        var allKeys = new HashSet<string>();
        allKeys.UnionWith(oldValues.Keys);
        allKeys.UnionWith(newValues.Keys);

        foreach (var key in allKeys)
        {
            var oldValue = oldValues.TryGetValue(key, out var ov) ? ov : null;
            var newValue = newValues.TryGetValue(key, out var nv) ? nv : null;

            if (!Equals(oldValue, newValue))
            {
                changes[key] = (oldValue, newValue);
            }
        }

        return changes;
    }
}
