namespace MudExtended.Models.Filtering;

/// <summary>
/// Field type for filtering.
/// </summary>
public enum FilterFieldType
{
    /// <summary>Text field</summary>
    Text,
    
    /// <summary>Number field</summary>
    Number,
    
    /// <summary>Date field</summary>
    Date,
    
    /// <summary>Date range field</summary>
    DateRange,
    
    /// <summary>Dropdown/Select field</summary>
    Select,
    
    /// <summary>Multi-select field</summary>
    MultiSelect,
    
    /// <summary>Boolean field</summary>
    Boolean,
    
    /// <summary>Checkbox group field</summary>
    CheckboxGroup
}
