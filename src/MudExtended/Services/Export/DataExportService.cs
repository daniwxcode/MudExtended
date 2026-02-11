using MudExtended.Models.Export;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace MudExtended.Services.Export;

/// <summary>
/// Default data export service implementation.
/// </summary>
public sealed class DataExportService : IDataExportService
{
    public async Task<byte[]> ExportAsync<T>(
        List<T> data,
        ExportFormat format,
        ExportOptions options,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(options);

        return format switch
        {
            ExportFormat.Csv => await ExportToCsvAsync(data, options, cancellationToken),
            ExportFormat.Json => await ExportToJsonAsync(data, options, cancellationToken),
            ExportFormat.Xml => await ExportToXmlAsync(data, options, cancellationToken),
            ExportFormat.Excel => await ExportToExcelAsync(data, options, cancellationToken),
            ExportFormat.Pdf => await ExportToPdfAsync(data, options, cancellationToken),
            _ => throw new NotSupportedException($"Format {format} is not supported")
        };
    }

    public string GetFileExtension(ExportFormat format) => format switch
    {
        ExportFormat.Csv => ".csv",
        ExportFormat.Json => ".json",
        ExportFormat.Xml => ".xml",
        ExportFormat.Excel => ".xlsx",
        ExportFormat.Pdf => ".pdf",
        _ => throw new NotSupportedException($"Format {format} is not supported")
    };

    public string GetMimeType(ExportFormat format) => format switch
    {
        ExportFormat.Csv => "text/csv",
        ExportFormat.Json => "application/json",
        ExportFormat.Xml => "application/xml",
        ExportFormat.Excel => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        ExportFormat.Pdf => "application/pdf",
        _ => throw new NotSupportedException($"Format {format} is not supported")
    };

    private async Task<byte[]> ExportToCsvAsync<T>(
        List<T> data,
        ExportOptions options,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var sb = new StringBuilder();

        if (options.IncludeHeaders)
        {
            var headers = GetHeaders<T>(options);
            sb.AppendLine(string.Join(",", headers));
        }

        int rowNumber = 0;
        foreach (var item in data)
        {
            if (options.MaxRows > 0 && rowNumber >= options.MaxRows)
                break;

            var values = GetRowValues(item, options);
            sb.AppendLine(string.Join(",", values));
            rowNumber++;
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    private async Task<byte[]> ExportToJsonAsync<T>(
        List<T> data,
        ExportOptions options,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var exportData = data
            .Take(options.MaxRows > 0 ? options.MaxRows : data.Count)
            .ToList();

        var json = JsonSerializer.Serialize(exportData, new JsonSerializerOptions { WriteIndented = true });
        return Encoding.UTF8.GetBytes(json);
    }

    private async Task<byte[]> ExportToXmlAsync<T>(
        List<T> data,
        ExportOptions options,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        sb.AppendLine("<root>");

        int rowNumber = 0;
        foreach (var item in data)
        {
            if (options.MaxRows > 0 && rowNumber >= options.MaxRows)
                break;

            sb.AppendLine($"  <item>");
            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(item);
                sb.AppendLine($"    <{prop.Name}>{EscapeXml(value?.ToString())}</{prop.Name}>");
            }
            sb.AppendLine($"  </item>");
            rowNumber++;
        }

        sb.AppendLine("</root>");
        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    private async Task<byte[]> ExportToExcelAsync<T>(
        List<T> data,
        ExportOptions options,
        CancellationToken cancellationToken)
    {
        // Excel export requires EPPlus or similar library
        // For now, return CSV as fallback
        await Task.CompletedTask;
        return await ExportToCsvAsync(data, options, cancellationToken);
    }

    private async Task<byte[]> ExportToPdfAsync<T>(
        List<T> data,
        ExportOptions options,
        CancellationToken cancellationToken)
    {
        // PDF export requires iTextSharp or similar library
        // For now, return JSON as fallback
        await Task.CompletedTask;
        return await ExportToJsonAsync(data, options, cancellationToken);
    }

    private static List<string> GetHeaders<T>(ExportOptions options)
    {
        if (options.Columns.Count > 0)
        {
            return options.Columns
                .Where(c => c.IsIncluded)
                .Select(c => options.HeaderMappings.TryGetValue(c.PropertyName, out var mapped) 
                    ? mapped 
                    : c.Header)
                .ToList();
        }

        return typeof(T).GetProperties()
            .Select(p => options.HeaderMappings.TryGetValue(p.Name, out var mapped) ? mapped : p.Name)
            .ToList();
    }

    private static List<string> GetRowValues<T>(T item, ExportOptions options)
    {
        var values = new List<string>();

        if (options.Columns.Count > 0)
        {
            foreach (var column in options.Columns.Where(c => c.IsIncluded))
            {
                var prop = typeof(T).GetProperty(column.PropertyName);
                if (prop != null)
                {
                    var value = prop.GetValue(item);
                    var formatted = column.Formatter?.Invoke(value) ?? value?.ToString() ?? string.Empty;
                    values.Add(CsvEscape(formatted));
                }
            }
        }
        else
        {
            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(item)?.ToString() ?? string.Empty;
                values.Add(CsvEscape(value));
            }
        }

        return values;
    }

    private static string CsvEscape(string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
        {
            return $"\"{value.Replace("\"", "\"\"")}\"";
        }

        return value;
    }

    private static string EscapeXml(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        return value
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&apos;");
    }
}
