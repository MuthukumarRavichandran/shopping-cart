namespace Price.Calculator.Contractors.Model;

/// <summary>
/// Cart data
/// </summary>
public record Cart
{
    /// <summary>
    /// Item name eg:Apple, Orange
    /// </summary>
    public required string ItemName { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    public required int Quantity { get; set; }
}
