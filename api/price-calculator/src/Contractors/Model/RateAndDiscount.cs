namespace Price.Calculator.Contractors.Model;

public record RateAndDiscount
{
    /// <summary>
    /// Item
    /// </summary>
    public required string Item { get; set; }

    /// <summary>
    /// Rate of each item
    /// </summary>
    public required int Rate { get; set; }

    /// <summary>
    /// Discount of each item
    /// </summary>
    public required int Discount { get; set; }
}
