using MudBlazor;
using MudExtended.Enums;

namespace MudExtended.Models.Status;

/// <summary>
/// Donn�es pour une carte statistique.
/// </summary>
public record StatCardData
{
    /// <summary>Libell� de la statistique.</summary>
    public required string Label { get; init; }

    /// <summary>Valeur principale.</summary>
    public required object Value { get; init; }

    /// <summary>Sous-libell� optionnel.</summary>
    public string? SubLabel { get; init; }

    /// <summary>Sous-valeur optionnelle.</summary>
    public object? SubValue { get; init; }

    /// <summary>Ic�ne Material.</summary>
    public string Icon { get; init; } = Icons.Material.Filled.Analytics;

    /// <summary>Couleur de la carte.</summary>
    public Color Color { get; init; } = Color.Primary;

    /// <summary>Type de formatage de la valeur.</summary>
    public StatValueType ValueType { get; init; } = StatValueType.Number;

    /// <summary>�l�vation de la carte.</summary>
    public int Elevation { get; init; } = 2;

    /// <summary>Classes CSS additionnelles.</summary>
    public string? Class { get; init; }
}
