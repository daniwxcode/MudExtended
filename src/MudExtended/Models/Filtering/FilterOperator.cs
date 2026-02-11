namespace MudExtended.Models.Filtering;

/// <summary>
/// Filter operators.
/// </summary>
public enum FilterOperator
{
    /// <summary>Equals</summary>
    Equals,
    
    /// <summary>Not equals</summary>
    NotEquals,
    
    /// <summary>Contains</summary>
    Contains,
    
    /// <summary>Not contains</summary>
    NotContains,
    
    /// <summary>Starts with</summary>
    StartsWith,
    
    /// <summary>Ends with</summary>
    EndsWith,
    
    /// <summary>Greater than</summary>
    GreaterThan,
    
    /// <summary>Less than</summary>
    LessThan,
    
    /// <summary>Greater than or equals</summary>
    GreaterThanOrEquals,
    
    /// <summary>Less than or equals</summary>
    LessThanOrEquals,
    
    /// <summary>Between</summary>
    Between,
    
    /// <summary>In list</summary>
    In,
    
    /// <summary>Is empty</summary>
    IsEmpty,
    
    /// <summary>Is not empty</summary>
    IsNotEmpty
}
