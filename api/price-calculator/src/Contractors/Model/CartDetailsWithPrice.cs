namespace Price.Calculator.Contractors.Model;

/// <summary>
/// CartDetails
/// </summary>
public record CartDetailsWithPrice
{
    /// <summary>
    /// Item name Ex: apple, orange
    /// </summary>
    public required string Item { get; init; }

    /// <summary>
    /// Quantity
    /// </summary>
    public int Quantity { get; init; }

    /// <summary>
    /// Rate
    /// </summary>
    public int Rate { get; init; }

    /// <summary>
    /// Discount for each item in percentage
    /// </summary>
    public int Discount { get; init; }

    /// <summary>
    /// Final price after the discount
    /// </summary>
    public decimal Price { get; init; }
}
