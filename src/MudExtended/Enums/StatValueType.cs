namespace MudExtended.Enums;

/// <summary>
/// Types de valeur pour les cartes statistiques.
/// </summary>
public enum StatValueType
{
    /// <summary>Nombre entier format� (ex: 1 234 567).</summary>
    Number,

    /// <summary>Montant mon�taire (ex: 1 234 567 FCFA).</summary>
    Currency,

    /// <summary>Pourcentage (ex: 45%).</summary>
    Percentage,

    /// <summary>Nombre d�cimal (ex: 1 234,56).</summary>
    Decimal,

    /// <summary>Valeur affich�e telle quelle.</summary>
    Raw
}
