using MudExtended.Models.Export;

namespace MudExtended.Services.Export;

/// <summary>
/// Service for data export functionality.
/// </summary>
public interface IDataExportService
{
    /// <summary>
    /// Export data to specified format.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="data">Data to export</param>
    /// <param name="format">Target format</param>
    /// <param name="options">Export options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Exported data as byte array</returns>
    Task<byte[]> ExportAsync<T>(
        List<T> data,
        ExportFormat format,
        ExportOptions options,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get file extension for format.
    /// </summary>
    string GetFileExtension(ExportFormat format);

    /// <summary>
    /// Get MIME type for format.
    /// </summary>
    string GetMimeType(ExportFormat format);
}
